using EPOOutline;
using Game.Scripts.Interactable;
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

        public void ShowUi()
        {
        }
        
        public Transform GetTransform() => gameObject.transform;

        #endregion
    }
}
