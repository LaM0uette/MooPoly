using Game.Scripts.Generic.PlayerPref;
using Game.Scripts.Interactables;

namespace Game.Scripts.Ui.World.Button.Reset
{
    public class FountainReset : Interactable
    {
        #region Functions

        public override void Interact()
        {
            PlayerPrefsSave.DeletePlayerPositionMenu();
        }
        
        public override void ShowOutline(bool value)
        {
            Outline.enabled = value;
        }

        #endregion
    }
}
