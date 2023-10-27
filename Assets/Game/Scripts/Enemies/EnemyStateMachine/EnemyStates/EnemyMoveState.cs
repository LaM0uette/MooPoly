namespace Game.Scripts.Enemies.EnemyStateMachine.EnemyStates
{
    public class EnemyMoveState : EnemyBaseState
    {
        #region Statements

        public EnemyMoveState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
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
            Move(EnemyStateMachine.EnemyData.MoveSpeed);
            
            AnimatorSetFloat(EnemyStateMachine.SpeedHash, 1);
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