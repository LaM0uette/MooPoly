using UnityEngine;

namespace Game.Scripts.Turrets.TurretStateMachine.TurretStates
{
    public class TurretBuildState : TurretBaseState
    {
        #region Statements
        
        private Vector3 _initialPos;
        private Vector3 _finalPos;

        public TurretBuildState(TurretStateMachine turretStateMachine) : base(turretStateMachine)
        {
        }

        #endregion

        #region Events

        public override void Enter()
        {
            _initialPos = TurretStateMachine.InitialPos.position;
            _finalPos = _initialPos - _initialPos;
            
            TurretStateMachine.transform.position = _initialPos;
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
            return TurretStateMachine.transform.position.y <= _finalPos.y;
        }

        private void MoveUpward(float deltaTime)
        {
            TurretStateMachine.transform.position += Vector3.up * (deltaTime * TurretStateMachine.Turret.BuildTime);
        }

        #endregion
    }
}
