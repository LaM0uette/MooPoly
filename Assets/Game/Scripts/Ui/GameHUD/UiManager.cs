using System;
using Game.Scripts.Player.Controller;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Scripts.Ui.GameHUD
{
    public class UiManager : MonoBehaviour
    {
        #region Statements
        
        public static Action OnUiManager;
        
        [SerializeField] private PlayerController _playerController;

        private UIDocument _uiDocument;
        private VisualElement _root;
        private VisualElement _veTurretsToBuild;

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            _root = _uiDocument.rootVisualElement;
            _veTurretsToBuild = _root.Query("VE_TurretsToBuild");
            
            _veTurretsToBuild.style.visibility = Visibility.Hidden;
            OnUiManager += Show;
        }

        #endregion

        #region Functions

        private void Show()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
            
            _veTurretsToBuild.style.visibility = Visibility.Visible;
            SetupButtons();
        }

        private void SetupButtons()
        {
            var btnClose = _veTurretsToBuild.Q<Button>("Btn_Close");
            var btnCrossBow = _veTurretsToBuild.Q<Button>("Btn_CrossBow");
            var btnMiniGun = _veTurretsToBuild.Q<Button>("Btn_MiniGun");
            var btnNailGun = _veTurretsToBuild.Q<Button>("Btn_NailGun");
            
            btnClose.RegisterCallback<ClickEvent>(_ => { Close(); } );
            btnCrossBow.RegisterCallback<ClickEvent>(_ => { BtnTurret(0); } );
            btnMiniGun.RegisterCallback<ClickEvent>(_ => { BtnTurret(1); } );
            btnNailGun.RegisterCallback<ClickEvent>(_ => { BtnTurret(2); } );
        }
        
        private void BtnTurret(int value)
        {
            _playerController.Temp(value);
            Close();
        }
        
        private void Close()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
            
            _veTurretsToBuild.style.visibility = Visibility.Hidden;
        }

        #endregion
    }
}
