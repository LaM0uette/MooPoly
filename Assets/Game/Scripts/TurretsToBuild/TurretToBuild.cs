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

        public override void ShowOutline(bool value)
        {
            switch (value)
            {
                case true when Outline.isActiveAndEnabled:
                case false when !Outline.isActiveAndEnabled:
                    return;
                default:
                    Outline.enabled = value;
                    break;
            }
        }

        public override void Destroy()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}
