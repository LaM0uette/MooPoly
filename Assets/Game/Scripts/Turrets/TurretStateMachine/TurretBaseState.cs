using Game.Scripts.BaseStateMachine;

namespace Game.Scripts.Turrets.TurretStateMachine
{
    public abstract class TurretBaseState : State
    {
        #region Statements

        protected readonly TurretStateMachine TurretStateMachine;
        
        protected TurretBaseState(TurretStateMachine turretStateMachine)
        {
            TurretStateMachine = turretStateMachine;
        }

        #endregion
    }
}
