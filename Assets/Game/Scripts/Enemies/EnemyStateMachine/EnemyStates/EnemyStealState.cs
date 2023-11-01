namespace Game.Scripts.Enemies.EnemyStateMachine.EnemyStates
{
    public class EnemyStealState : EnemyBaseState
    {
        #region Statements

        public EnemyStealState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        #endregion

        #region Events

        public override void Enter()
        {
            //EnemyStateMachine.Enemy.IsDead = true;
            EnemyStateMachine.TransitionToAnimation(EnemyStateMachine.AttackHash);
        }

        public override void Tick(float deltaTime)
        {
            if (EnemyStateMachine.IsTransitioning || !HasAnimationReachedStage(.9f)) return;

            EnemyStateMachine.StealDead();
        }

        public override void Exit()
        {
        }

        #endregion
    }
}