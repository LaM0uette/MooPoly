using Game.Scripts.Player.Controller;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Scripts.Ui.GameHUD.Screens
{
    public class UIS_TurretsToBuild : UiScreen
    {
        #region Elements

        private const string _turretsToBuildHash = "TurretsToBuild";
        private const string _btnCloseHash = "Btn_Close";
        private const string _btnCrossBowHash = "Btn_CrossBow";
        private const string _btnMiniGunHash = "Btn_MiniGun";
        private const string _btnNailGunHash = "Btn_NailGun";
        
        private VisualElement _turretsToBuild;
        private Button _btnClose;
        private Button _btnCrossBow;
        private Button _btnMiniGun;
        private Button _btnNailGun;

        #endregion

        #region Statements

        [SerializeField] private PlayerController _playerController;

        #endregion

        #region Virtual

        protected override void SetVisualElements()
        {
            base.SetVisualElements();
            
            _turretsToBuild = Root.Q(_turretsToBuildHash);
            _btnClose = Root.Q<Button>(_btnCloseHash);
            _btnCrossBow = Root.Q<Button>(_btnCrossBowHash);
            _btnMiniGun = Root.Q<Button>(_btnMiniGunHash);
            _btnNailGun = Root.Q<Button>(_btnNailGunHash);
        }
        
        protected override void RegisterButtonCallbacks()
        {
            _btnClose.RegisterCallback<ClickEvent>(_ => { Close(); } );
            _btnCrossBow.RegisterCallback<ClickEvent>(_ => { BtnTurret(0); } );
            _btnMiniGun.RegisterCallback<ClickEvent>(_ => { BtnTurret(1); } );
            _btnNailGun.RegisterCallback<ClickEvent>(_ => { BtnTurret(2); } );
        }

        #endregion

        #region Functions

        private void BtnTurret(int value)
        {
            _playerController.BuildTurret(value);
            Close();
        }
        
        private void Close()
        {
            HideScreen();
        }

        #endregion
    }
}
