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
            //TurretStateMachine.OnUpdateRepeating += SearchEnemy;
        }

        public override void TickFixed(float deltaTime)
        {
            Rotate();
        }
        
        public override void CheckState()
        {
        }

        public override void Exit()
        {
            //TurretStateMachine.OnUpdateRepeating -= SearchEnemy;
        }

        #endregion

        #region Functions

        private void Rotate()
        {
            var eulers = Vector3.up * (TurretStateMachine.Turret.RotationSpeed * Time.deltaTime);
            TurretStateMachine.TurretMobileTransform.Rotate(eulers);
        }

        #endregion
    }
}
