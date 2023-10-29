namespace Game.Scripts.Enemies.EnemyStateMachine.EnemyStates
{
    public class EnemyDieState : EnemyBaseState
    {
        #region Statements

        public EnemyDieState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        #endregion

        #region Events

        public override void Enter()
        {
            EnemyStateMachine.Enemy.IsDead = true;
            EnemyStateMachine.TransitionToAnimation(EnemyStateMachine.DieHash);
        }

        public override void Tick(float deltaTime)
        {
            if (EnemyStateMachine.IsTransitioning || !HasAnimationReachedStage(.9f)) return;

            EnemyStateMachine.Dead();
        }

        public override void Exit()
        {
        }

        #endregion
    }
}