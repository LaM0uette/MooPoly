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
        
        public static readonly int IdleState = Animator.StringToHash("IdleSubState.Idle");
        public static readonly int MoveState = Animator.StringToHash("MoveBlendTree");
        
        #endregion
        
        #region Statements
        
        // Components
        public Animator Animator { get; private set; }
        public PlayerInputsReader Inputs { get; private set; }
        public CharacterController Controller { get; private set; }
        
        // Properties
        public bool IsTransitioning { get; private set; }
        
        [Title("Move")]
        public float MoveSpeed = 4f;

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
    }
}
