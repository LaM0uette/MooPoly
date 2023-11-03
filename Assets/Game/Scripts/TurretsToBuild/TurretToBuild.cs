using Game.Scripts.Interactables;
using Game.Scripts.Ui.GameHUD;

namespace Game.Scripts.TurretsToBuild
{
    public class TurretToBuild : Interactable
    {
        #region Functions

        public override void Interact()
        {
            GameHUDManager.OnUiManager?.Invoke();
        }

        public override void Destroy()
        {
            Destroy(gameObject, 0);
        }

        #endregion
    }
}
