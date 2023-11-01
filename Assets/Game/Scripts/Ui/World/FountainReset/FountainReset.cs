using Game.Scripts.Generic.PlayerPref;
using Game.Scripts.Interactables;

namespace Game.Scripts.Ui.World.FountainReset
{
    public class FountainReset : Interactable
    {
        #region Functions

        public override void Interact()
        {
            PlayerPrefsSave.DeletePlayerPositionMenu();
        }

        #endregion
    }
}
