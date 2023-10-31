using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Scripts.Ui.GameHUD
{
    public abstract class UiScreen : MonoBehaviour
    {
        #region Statements

        [SerializeField] protected string ScreenName;
        [SerializeField] protected UIDocument Document;
        
        protected VisualElement Screen;
        protected VisualElement Root;

        protected virtual void Awake()
        {
            SetVisualElements();
            RegisterButtonCallbacks();
        }

        #endregion

        #region virtual

        protected virtual void SetVisualElements()
        {
            if (Document is not null)
                Root = Document.rootVisualElement;

            Screen = GetVisualElement(ScreenName);
        }
        
        protected virtual void RegisterButtonCallbacks() { }
        
        public virtual void ShowScreen(bool lockCursor = true)
        {
            if (lockCursor)
            {
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                UnityEngine.Cursor.visible = true;
            }
            
            ShowVisualElement(Screen, true);
        }

        public virtual void HideScreen(bool lockCursor = true)
        {
            if (lockCursor)
            {
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                UnityEngine.Cursor.visible = false;
            }

            if (IsVisible())
                ShowVisualElement(Screen, false);
        }

        #endregion

        #region Functions

        private VisualElement GetVisualElement(string elementName)
        {
            if (string.IsNullOrEmpty(elementName) || Root is null)
                return null;

            return Root.Q(elementName);
        }
        
        public bool IsVisible()
        {
            if (Screen is null)
                return false;

            return Screen.style.display == DisplayStyle.Flex;
        }

        private static void ShowVisualElement(VisualElement visualElement, bool state)
        {
            if (visualElement is null)
                return;

            visualElement.style.display = state ? DisplayStyle.Flex : DisplayStyle.None;
        }

        #endregion
    }
}
