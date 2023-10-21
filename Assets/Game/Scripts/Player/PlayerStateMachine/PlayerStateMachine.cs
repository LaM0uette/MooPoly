using System.Collections;
using Game.Scripts.BaseStateMachine;
using Game.Scripts.Player.PlayerInputs;
using Game.Scripts.Player.PlayerStateMachine.PlayerStates;
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
            
        // Variables
        public static readonly int SpeedHash = Animator.StringToHash("Speed");
        
        #endregion
        
        #region Statements
        
        // Components
        public Animator Animator { get; private set; }
        public PlayerInputsReader Inputs { get; private set; }
        public CharacterController Controller { get; private set; }
        
        // Properties
        public bool IsTransitioning { get; private set; }
        public Vector3 Velocity { get; set; }
        
        [Title("Move")]
        public float MoveSpeed = 4f;
        public float RotationMoveSpeed = 10f;
        
        [Header("Parameters")]
        public float Gravity = -16f;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Inputs = GetComponent<PlayerInputsReader>();
            Controller = GetComponent<CharacterController>();
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
