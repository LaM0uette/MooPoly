using System;
using System.Collections.Generic;
using Game.Scripts.Ui.GameHUD.Screens;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Ui.GameHUD
{
    public class UiManager : MonoBehaviour
    {
        #region Statements
        
        public static Action OnUiManager;
        
        [SerializeField] private UIS_TurretsToBuild _uisTurretsToBuild;
        
        private readonly List<UiScreen> _allModalUIScreens = new();

        private void Awake()
        {
            SetupModalScreens();
        }

        #endregion

        #region Events

        private void OnEnable()
        {
            OnUiManager += ShowTurretsToBuildUIScreen;
        }

        private void OnDisable()
        {
            OnUiManager -= ShowTurretsToBuildUIScreen;
        }

        #endregion

        #region Functions

        private void SetupModalScreens()
        {
            _allModalUIScreens?.Add(_uisTurretsToBuild);
        }
        
        private void ShowTurretsToBuildUIScreen() => ShowModalScreen(_uisTurretsToBuild);
        
        private void ShowModalScreen(Object modalScreen)
        {
            foreach (var modalUIScreen in _allModalUIScreens)
            {
                if (modalUIScreen == modalScreen)
                    modalUIScreen.ShowScreen();
                else
                    modalUIScreen.HideScreen();
            }
        }
        
        #endregion
    }
}
