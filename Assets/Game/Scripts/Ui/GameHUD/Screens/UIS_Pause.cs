using Game.Scripts._Data.SceneData;
using Game.Scripts.Scenes;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Scripts.Ui.GameHUD.Screens
{
    public class UIS_Pause : UiScreen
    {
        #region Elements
        
        [SerializeField] private SceneData _sceneData;

        private const string _returnToMenuHash = "Btn_ReturnToMenu";
        
        private Button _returnToMenu;

        #endregion

        #region Virtual

        protected override void SetVisualElements()
        {
            base.SetVisualElements();
            
            _returnToMenu = Root.Q<Button>(_returnToMenuHash);
            _returnToMenu.clicked += () => { ScenesManager.LoadScene(_sceneData); };
        }

        #endregion
    }
}
