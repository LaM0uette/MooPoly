using EPOOutline;
using Game.Scripts.Interactable;
using Game.Scripts.Ui.TurretsToBuild;
using UnityEngine;

namespace Game.Scripts.TurretsToBuild
{
    public class TurretToBuild : MonoBehaviour, IInteract
    {
        #region Statements

        [SerializeField] private Outlinable _outlinable;

        #endregion
        
        #region Functions

        public void Interact()
        {
            UiManager.OnUiManager?.Invoke();
        }

        public void ShowOutline(bool value)
        {
            switch (value)
            {
                case true when _outlinable.isActiveAndEnabled:
                case false when !_outlinable.isActiveAndEnabled:
                    return;
                default:
                    _outlinable.enabled = value;
                    break;
            }
        }
        
        public Transform GetTransform() => gameObject.transform;

        public void Destroy()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}
