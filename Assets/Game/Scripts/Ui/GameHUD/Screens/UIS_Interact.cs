using UnityEngine.UIElements;

namespace Game.Scripts.Ui.GameHUD.Screens
{
    public class UIS_Interact : UiScreen
    {
        #region Elements

        private const string _InteractHash = "Interact";
        
        private VisualElement _Interact;

        #endregion

        #region Virtual

        protected override void SetVisualElements()
        {
            base.SetVisualElements();
            
            _Interact = Root.Q(_InteractHash);
        }

        #endregion
    }
}
