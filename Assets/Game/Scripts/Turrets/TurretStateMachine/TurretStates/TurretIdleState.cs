using Game.Scripts._Data.EnemyData;
using Game.Scripts._Data.TurretData;
using Game.Scripts.Enemies.EnemyStateMachine;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Scripts.Turrets.TurretStateMachine.TurretStates
{
    public class TurretIdleState : TurretBaseState
    {
        #region Statements

        public TurretIdleState(TurretStateMachine turretStateMachine) : base(turretStateMachine)
        {
        }

        #endregion

        #region Events

        public override void Enter()
        {
            TurretStateMachine.OnUpdateRepeating += SearchEnemy;
        }

        public override void TickFixed(float deltaTime)
        {
            Rotate();
        }

        public override void Exit()
        {
            TurretStateMachine.OnUpdateRepeating -= SearchEnemy;
        }

        #endregion

        #region Functions

        private void Rotate()
        {
            var eulers = Vector3.up * (TurretStateMachine.Turret.RotationSpeed * Time.deltaTime);
            TurretStateMachine.TurretMobileTransform.Rotate(eulers);
        }
        
        private void SearchEnemy()
        {
            var enemy = GetClosestEnemy();
            
            if (enemy is null || !CanTouchEnemy(enemy)) return;
            
            TurretStateMachine.SwitchState(new TurretShootState(TurretStateMachine, enemy));
        }
        
        [CanBeNull]
        private EnemyStateMachine GetClosestEnemy()
        {
            var turretPosition = TurretStateMachine.transform.position;
            var turretRange = TurretStateMachine.Turret.Range;
    
            var colliders = Physics.OverlapSphere(turretPosition, turretRange, TurretStateMachine.EnemyLayer);

            var shorterDistance = Mathf.Infinity;
            EnemyStateMachine enemy = null;

            foreach (var collider in colliders)
            {
                var distance = Vector3.Distance(turretPosition, collider.transform.position);
                if (distance >= shorterDistance) continue;
        
                shorterDistance = distance;
                enemy = collider.gameObject.GetComponent<EnemyStateMachine>();
            }

            return enemy;
        }
        
        private bool CanTouchEnemy(EnemyStateMachine enemy)
        {
            var turretMode = TurretStateMachine.Turret.Type;
            var enemyType = enemy.Enemy.Type;

            return turretMode switch
            {
                TurretType.Land => enemyType is EnemyType.Land or EnemyType.BossLand,
                TurretType.Air => enemyType is EnemyType.Air or EnemyType.BossAir,
                TurretType.Both => true,
                _ => false
            };
        }

        #endregion
    }
}
