using UnityEngine.UIElements;

namespace Game.Scripts.Ui.GameHUD.Screens
{
    public class UIS_LevelCoins : UiScreen
    {
        #region Elements

        private const string _levelCoinsHash = "LevelCoins";
        private const string _lbCoinsHash = "Lb_Coins";
        
        private VisualElement _levelCoins;
        private Label _lbCoins;

        #endregion

        #region Virtual

        protected override void SetVisualElements()
        {
            base.SetVisualElements();
            
            _levelCoins = Root.Q(_levelCoinsHash);
            _lbCoins = Root.Q<Label>(_lbCoinsHash);
            _lbCoins.text = 000.ToString();
        }

        #endregion

        #region Functions

        public void UpdateLevelCoins(int coins)
        {
            _lbCoins.text = coins.ToString();
        }

        #endregion
    }
}
