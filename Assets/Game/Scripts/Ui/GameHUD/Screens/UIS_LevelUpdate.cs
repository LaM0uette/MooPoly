using UnityEngine.UIElements;

namespace Game.Scripts.Ui.GameHUD.Screens
{
    public class UIS_LevelUpdate : UiScreen
    {
        #region Elements

        private const string _levelUpdateHash = "LevelUpdate";
        private const string _lbLevelUpdateHash = "Lb_LevelUpdate";
        
        private VisualElement _levelUpdate;
        private Label _lbLevelUpdate;

        #endregion

        #region Virtual

        protected override void SetVisualElements()
        {
            base.SetVisualElements();
            
            _levelUpdate = Root.Q(_levelUpdateHash);
            _lbLevelUpdate = Root.Q<Label>(_lbLevelUpdateHash);
        }

        #endregion
        
        #region Functions

        public void UpdateText(string text)
        {
            _lbLevelUpdate.text = text;
        }

        #endregion
    }
}
