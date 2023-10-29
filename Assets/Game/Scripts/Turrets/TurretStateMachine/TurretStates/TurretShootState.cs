using Game.Scripts.Enemies.EnemyStateMachine;
using UnityEngine;

namespace Game.Scripts.Turrets.TurretStateMachine.TurretStates
{
    public class TurretShootState : TurretBaseState
    {
        #region Statements
        
        private readonly EnemyStateMachine _enemy;
        private readonly Vector3 _turretPosition;
        
        private float _fireRateCountdown;

        public TurretShootState(TurretStateMachine turretStateMachine, EnemyStateMachine enemy) : base(turretStateMachine)
        {
            _enemy = enemy;
            _turretPosition = TurretStateMachine.TurretHeadTransform.position;
        }

        #endregion

        #region Events

        public override void Enter()
        {
        }

        public override void CheckState()
        {
            if (_enemy.Enemy.IsDead) 
                TurretStateMachine.SwitchState(new TurretIdleState(TurretStateMachine));
        }
        
        public override void Tick(float deltaTime)
        {
            if (_enemy.Enemy.IsDead) return;
            
            CheckIfCanShoot();
        }

        public override void TickFixed(float deltaTime)
        {
            if (_enemy.Enemy.IsDead) return;
            
            LookAtEnemy();
        }
        
        public override void TickLate(float deltaTime)
        {
            if (_enemy.Enemy.IsDead) return;
            
            TurretStateMachine.DebugLine(_turretPosition, _enemy.Target.transform.position, Color.red);
        }

        public override void Exit()
        {
        }

        #endregion

        #region Functions
        
        private void LookAtEnemy()
        {
            var direction = _enemy.Target.transform.position - _turretPosition;
            var flatDirection = new Vector3(direction.x, 0, direction.z);
            var lookRotation = Quaternion.LookRotation(flatDirection);

            var rotation = Quaternion.Lerp(TurretStateMachine.TurretMobileTransform.rotation, lookRotation, Time.deltaTime * TurretStateMachine.Turret.RotationSpeed).eulerAngles;
            TurretStateMachine.TurretMobileTransform.rotation = Quaternion.Euler(0, rotation.y, 0);

            var angleX = Vector3.Angle(direction, flatDirection) * (direction.y < 0 ? -1 : 1);
            TurretStateMachine.TurretHeadTransform.localRotation = Quaternion.Euler(-angleX, 0, 0);
        }
        
        private void CheckIfCanShoot()
        {
            if (!CanShoot() || _enemy.Enemy.IsDead) return;

            TurretStateMachine.Turret.Shoot(_enemy);
        }
        
        private bool CanShoot()
        {
            if (_fireRateCountdown <= 0f)
            {
                _fireRateCountdown = 100f / TurretStateMachine.Turret.FireRate;
                return true;
            }
            
            _fireRateCountdown -= Time.deltaTime;
            return false;
        }
        
        #endregion
    }
}
