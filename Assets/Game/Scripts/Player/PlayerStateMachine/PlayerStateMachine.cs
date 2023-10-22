using System.Collections;
using Cinemachine;
using Game.Scripts.BaseStateMachine;
using Game.Scripts.Camera.Confiner;
using Game.Scripts.Player.PlayerInputs;
using Game.Scripts.Player.PlayerStateMachine.PlayerStates;
using Game.Scripts.StaticUtilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Player.PlayerStateMachine
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerInputsReader))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerStateMachine : StateMachine
    {
        #region AnimationHash
        
        // States
        public static readonly int IdleStateHash = Animator.StringToHash("IdleSubState.Idle");
        public static readonly int MoveStateHash = Animator.StringToHash("MoveBlendTree");
        public static readonly int JumpHash = Animator.StringToHash("Jump");
            
        // Variables
        public static readonly int SpeedHash = Animator.StringToHash("Speed");
        public static readonly int IdleBlendHash = Animator.StringToHash("IdleBlend");
        
        #endregion
        
        #region Statements
        
        // Components
        public Animator Animator { get; private set; }
        public PlayerInputsReader Inputs { get; private set; }
        public CharacterController Controller { get; private set; }
        public CinemachineVirtualCamera TopDownCamera { get; private set; }
        public CinemachineVirtualCamera MapCamera { get; private set; }
        public Confiner Confiner { get; private set; }
        
        // Properties
        public bool IsTransitioning { get; private set; }
        public Vector3 Velocity { get; set; }
        
        [Space, Title("Move")]
        public float MoveSpeed = 5f;
        public float RotationMoveSpeed = 10f;
        public float JumpForce = 6f;
        
        [Space, Title("Cinemachine")]
        public float MinZoom = 12f;
        public float MaxZoom = 25f;
        public float ZoomForce = 10f;
        
        [Space, Title("Parameters")]
        public float Gravity = -16f;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Inputs = GetComponent<PlayerInputsReader>();
            Controller = GetComponent<CharacterController>();
            
            var topDownCamera = GameObject.FindGameObjectWithTag(TagRef.TopDownCamera);
            TopDownCamera = topDownCamera.GetComponent<CinemachineVirtualCamera>();
            
            var mapCamera = GameObject.FindGameObjectWithTag(TagRef.MapCamera);
            MapCamera = mapCamera.GetComponent<CinemachineVirtualCamera>();
            
            var confiner = GameObject.FindGameObjectWithTag(TagRef.Confiner);
            Confiner = confiner.GetComponent<Confiner>();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            SwitchState(new PlayerIdleState(this));
        }

        #endregion

        #region Animations

        public void TransitionToAnimation(int animationId, float transitionDuration = .1f)
        {
            Animator.CrossFadeInFixedTime(animationId, transitionDuration);
            IsTransitioning = true;
            
            StartCoroutine(EndTransitionAfterDelay(transitionDuration));
        }
        
        private IEnumerator EndTransitionAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            IsTransitioning = false;
        }

        #endregion
        
        #region Checks

        public bool IsMoving() => !Inputs.MoveValue.Equals(Vector2.zero);
        public bool IsGrounded() => Controller.isGrounded;

        #endregion
    }
}
