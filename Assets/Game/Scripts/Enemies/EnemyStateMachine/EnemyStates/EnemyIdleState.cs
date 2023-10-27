namespace Game.Scripts.Enemies.EnemyStateMachine.EnemyStates
{
    public class EnemyIdleState : EnemyBaseState
    {
        #region Statements

        public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        #endregion

        #region Events

        public override void Enter()
        {
            EnemyStateMachine.TransitionToAnimation(EnemyStateMachine.MoveStateHash, .2f);
        }

        public override void Tick(float deltaTime)
        {
            Move();
        }
        
        public override void CheckState()
        {
        }

        public override void Exit()
        {
        }

        #endregion
    }
}