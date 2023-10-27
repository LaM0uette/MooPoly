using Game.Scripts.BaseStateMachine;

namespace Game.Scripts.Enemies.EnemyStateMachine
{
    public abstract class EnemyBaseState : State
    {
        #region Statements

        protected readonly Enemies.EnemyStateMachine.EnemyStateMachine EnemyStateMachine;
        
        protected EnemyBaseState(Enemies.EnemyStateMachine.EnemyStateMachine enemyStateMachine)
        {
            EnemyStateMachine = enemyStateMachine;
        }

        #endregion

        #region Functions

        protected void Move()
        {
            // TODO: Move enemy
        }

        #endregion
    }
}
