using Game.Scripts.Player.Controller;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Scripts.Ui.GameHUD.Screens
{
    public class UIS_TurretsToBuild : UiScreen
    {
        #region Elements

        private const string _turretsToBuildHash = "TurretsToBuild";
        private const string _ttbHash = "TTB";
        
        private VisualElement _turretsToBuild;
        private VisualElement _ttb;

        #endregion

        #region Statements

        [SerializeField] private PlayerController _playerController;

        #endregion

        #region Virtual

        protected override void SetVisualElements()
        {
            base.SetVisualElements();
            
            _turretsToBuild = Root.Q(_turretsToBuildHash);
            _ttb = Root.Q(_ttbHash);
            
            var turretsToBuild = _playerController.TurretsToBuild;

            for (var i = 0; i < turretsToBuild.Count; i++)
            {
                var turret = turretsToBuild[i];
                
                var button = new Button
                {
                    name = $"Btn_{turret.TurretData.Name}",
                    text = $"{turret.TurretData.Name}: {turret.TurretData.Cost}€"
                };

                var i1 = i;
                button.RegisterCallback<ClickEvent>(_ => { BtnTurret(i1); } );
                button.AddToClassList("turretButton");
                _ttb.Add(button);
            }
        }

        #endregion

        #region Functions

        private void BtnTurret(int value)
        {
            _playerController.BuildTurret(value);
            HideScreen();
        }

        #endregion
    }
}
