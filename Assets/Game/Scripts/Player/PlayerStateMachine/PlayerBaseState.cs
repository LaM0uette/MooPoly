using Cinemachine;
using Game.Scripts.BaseStateMachine;
using Game.Scripts.Player.Controller;
using Game.Scripts.Player.PlayerStateMachine.PlayerStates;
using UnityEngine;

namespace Game.Scripts.Player.PlayerStateMachine
{
    public abstract class PlayerBaseState : State
    {
        #region Statements

        protected readonly PlayerStateMachine PlayerStateMachine;
        
        private readonly CinemachineFramingTransposer _topDownCamera;
        
        protected PlayerBaseState(PlayerStateMachine playerStateMachine)
        {
            PlayerStateMachine = playerStateMachine;
            
            _topDownCamera = PlayerStateMachine.TopDownCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            
            UpdateColliderSizeBasedOnZoom(_topDownCamera.m_CameraDistance);
        }

        #endregion
        
        #region Gravity

        protected void ApplyGravity(float multiplier = 2f)
        {
            if (!(PlayerStateMachine.Velocity.y > PlayerStateMachine.Gravity)) return;
            
            var newVelocity = PlayerStateMachine.Velocity;
            newVelocity.y += Physics.gravity.y * multiplier * Time.deltaTime;
            PlayerStateMachine.Velocity = newVelocity;
        }
        
        protected void ResetVelocity()
        {
            PlayerStateMachine.Velocity = Vector3.zero;
        }

        #endregion
        
        #region Animations

        protected void AnimatorSetFloat(int id, float value, float dampTime)
        {
            PlayerStateMachine.Animator.SetFloat(id, value, dampTime, Time.deltaTime);
        }
        
        protected void AnimatorSetInt(int id, int value)
        {
            PlayerStateMachine.Animator.SetInteger(id, value);
        }
        
        protected float AnimatorGetFloat(int id)
        {
            return PlayerStateMachine.Animator.GetFloat(id);
        }
        
        protected bool HasAnimationReachedStage(float value, int layerIndex = 0)
        {
            var state = PlayerStateMachine.Animator.GetCurrentAnimatorStateInfo(layerIndex);
            var normalizedTime = Mathf.Repeat(state.normalizedTime,1f);

            return normalizedTime > value;
        }

        #endregion

        #region PlayerMovements

        protected void Move(float targetSpeed)
        {
            var moveValue = PlayerStateMachine.Inputs.MoveValue;
            var movement = new Vector3(moveValue.x, 0, moveValue.y).normalized;

            if (movement != Vector3.zero)
            {
                PlayerStateMachine.transform.rotation = Quaternion.Slerp(
                    PlayerStateMachine.transform.rotation, 
                    Quaternion.LookRotation(movement), 
                    PlayerStateMachine.RotationMoveSpeed * Time.deltaTime
                );
        
                if (Vector3.Dot(PlayerStateMachine.transform.forward, movement) < 1.0f)
                {
                    targetSpeed *= 0.8f;
                }
            }

            PlayerStateMachine.Controller.Move(movement * (targetSpeed * Time.deltaTime) + new Vector3(0, PlayerStateMachine.Velocity.y * Time.deltaTime, 0));
        }

        protected void CameraZoom()
        {
            var zoomValue = PlayerStateMachine.Inputs.ZoomValue;
            if (zoomValue.Equals(0)) return;

            _topDownCamera.m_CameraDistance += zoomValue * PlayerStateMachine.ZoomForce * Time.deltaTime;
                
            if (_topDownCamera.m_CameraDistance <= PlayerStateMachine.MinZoom) _topDownCamera.m_CameraDistance = PlayerStateMachine.MinZoom;
            if (_topDownCamera.m_CameraDistance >= PlayerStateMachine.MaxZoom) _topDownCamera.m_CameraDistance = PlayerStateMachine.MaxZoom;
            
            UpdateColliderSizeBasedOnZoom(_topDownCamera.m_CameraDistance);
        }
        
        private void UpdateColliderSizeBasedOnZoom(float currentZoom)
        {
            var t = Mathf.InverseLerp(PlayerStateMachine.MinZoom, PlayerStateMachine.MaxZoom, currentZoom);
            var colliderSize = Mathf.Lerp(PlayerStateMachine.Confiner.MaxBoxWidth, PlayerStateMachine.Confiner.MinBoxWidth, t);

            var boxColliderSize = PlayerStateMachine.Confiner.BoxCollider.size;
            boxColliderSize = new Vector3(colliderSize, boxColliderSize.y, boxColliderSize.z);
            PlayerStateMachine.Confiner.BoxCollider.size = boxColliderSize;
        }

        #endregion

        #region States

        protected void Jump() => PlayerStateMachine.SwitchState(new PlayerJumpState(PlayerStateMachine));
        protected static void Interact() => PlayerController.CurrentInteract?.Interact();
        protected void LevelMap() => PlayerStateMachine.SwitchState(new PlayerMapState(PlayerStateMachine));

        #endregion
    }
}
