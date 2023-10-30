using System;
using System.Collections.Generic;
using Game.Scripts.Levels;
using Game.Scripts.Ui.GameHUD.Screens;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Ui.GameHUD
{
    public class GameHUDManager : MonoBehaviour
    {
        #region Statements
        
        public static Action OnUiManager;
        
        [SerializeField] private UIS_TurretsToBuild _uisTurretsToBuild;
        [SerializeField] private UIS_LevelCoins _uisLevelCoins;
        
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
            LevelManager.OnLevelStart += ShowLevelCoinsUIScreen;
            LevelManager.OnLevelEnd += HideLevelCoinsUIScreen;
        }

        private void OnDisable()
        {
            OnUiManager -= ShowTurretsToBuildUIScreen;
            LevelManager.OnLevelStart -= ShowLevelCoinsUIScreen;
            LevelManager.OnLevelEnd -= HideLevelCoinsUIScreen;
        }

        #endregion

        #region Functions

        private void SetupModalScreens()
        {
            _allModalUIScreens?.Add(_uisTurretsToBuild);
        }
        
        private void ShowTurretsToBuildUIScreen() => ShowModalScreen(_uisTurretsToBuild);
        
        private void ShowLevelCoinsUIScreen() => _uisLevelCoins.ShowScreen();
        private void HideLevelCoinsUIScreen() => _uisLevelCoins.HideScreen();
        
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