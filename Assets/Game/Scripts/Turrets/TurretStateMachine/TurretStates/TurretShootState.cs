using Game.Scripts.Enemies.EnemyStateMachine;
using UnityEngine;

namespace Game.Scripts.Turrets.TurretStateMachine.TurretStates
{
    public class TurretShootState : TurretBaseState
    {
        #region Statements
        
        private readonly EnemyStateMachine _enemy;

        public TurretShootState(TurretStateMachine turretStateMachine, EnemyStateMachine enemy) : base(turretStateMachine)
        {
            _enemy = enemy;
        }

        #endregion

        #region Events

        public override void Enter()
        {
        }

        public override void Tick(float deltaTime)
        {
            if (_enemy.IsDead) 
                TurretStateMachine.SwitchState(new TurretIdleState(TurretStateMachine));
        }
        
        public override void TickLate(float deltaTime)
        {
            if (_enemy.IsDead) return;
            
            TurretStateMachine.DebugLine(TurretStateMachine.transform.position, _enemy.transform.position, Color.red);
        }

        public override void Exit()
        {
        }

        #endregion

        #region Functions

        #endregion
    }
}
