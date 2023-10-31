using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Levels;
using Game.Scripts.Player.Controller;
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
        [SerializeField] private UIS_Interact _uisInteract;
        [SerializeField] private UIS_LevelUpdate _uisLevelUpdate;
        
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
            LevelManager.OnLevelUpdate += ShowLevelUpdateUIScreen;
            
            PlayerController.OnTriggerInteract += InteractUIScreenHandle;
        }

        private void OnDisable()
        {
            OnUiManager -= ShowTurretsToBuildUIScreen;
            LevelManager.OnLevelStart -= ShowLevelCoinsUIScreen;
            LevelManager.OnLevelEnd -= HideLevelCoinsUIScreen;
            LevelManager.OnLevelUpdate += ShowLevelUpdateUIScreen;
            
            PlayerController.OnTriggerInteract -= InteractUIScreenHandle;
        }

        #endregion

        #region Functions

        private void SetupModalScreens()
        {
            _allModalUIScreens?.Add(_uisTurretsToBuild);
        }
        
        private void ShowTurretsToBuildUIScreen() => ShowModalScreen(_uisTurretsToBuild);
        
        private void ShowLevelCoinsUIScreen() => _uisLevelCoins.ShowScreen(false);
        private void HideLevelCoinsUIScreen() => _uisLevelCoins.HideScreen(false);

        private void ShowLevelUpdateUIScreen(string text)
        {
            _uisLevelUpdate.UpdateText(text);
            _uisLevelUpdate.ShowScreen(false);
            //StartCoroutine(nameof(FuncWithDelay), 3f);
        }
        private void HideLevelUpdateUIScreen() => _uisLevelUpdate.HideScreen(false);
        
        private void InteractUIScreenHandle(bool value)
        {
            if (value)
                _uisInteract.ShowScreen(false);
            else
                _uisInteract.HideScreen(false);
        }
        
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
        
        private IEnumerator FuncWithDelay(float delay)
        {
            var remainingTime = delay;
            
            while (remainingTime > 0)
            {
                yield return new WaitForSeconds(1);
                remainingTime--;
            }

            HideLevelUpdateUIScreen();
        }
    }
}
