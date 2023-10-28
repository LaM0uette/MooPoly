using UnityEngine;

namespace Game.Scripts.Turrets.TurretStateMachine.TurretStates
{
    public class TurretBuildState : TurretBaseState
    {
        #region Statements

        public TurretBuildState(TurretStateMachine turretStateMachine) : base(turretStateMachine)
        {
        }

        #endregion

        #region Events

        public override void Enter()
        {
            var initialPos = TurretStateMachine.InitialPos.position;
            TurretStateMachine.transform.position = initialPos;
        }

        public override void TickFixed(float deltaTime)
        {
            if (IsBelowFinalPosition())
                MoveUpward(deltaTime);
            else
                TurretStateMachine.SwitchState(new TurretIdleState(TurretStateMachine));
        }

        public override void Exit()
        {
        }

        #endregion

        #region Functions

        private bool IsBelowFinalPosition()
        {
            return TurretStateMachine.transform.position.y <= 0;
        }

        private void MoveUpward(float deltaTime)
        {
            TurretStateMachine.transform.position += Vector3.up * (deltaTime * TurretStateMachine.Turret.BuildTime);
        }

        #endregion
    }
}
