using Game.Scripts.Interactables;
using Game.Scripts.Ui.TurretsToBuild;

namespace Game.Scripts.TurretsToBuild
{
    public class TurretToBuild : Interactable
    {
        #region Functions

        public override void Interact()
        {
            UiManager.OnUiManager?.Invoke();
        }

        public override void Enter()
        {
            if (Outline.isActiveAndEnabled) return;
            base.Enter();
        }
        
        public override void Exit()
        {
            if (!Outline.isActiveAndEnabled) return;
            base.Exit();
        }

        public override void Destroy()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}
