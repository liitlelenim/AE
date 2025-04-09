using AE.Manager.Locator;
using UnityEngine;

namespace AE.Manager
{
    public class CursorManager : MonoBehaviour, IManager
    {
        public bool IsCursorActive { get; private set; } = false;
        [SerializeField] private bool startupDefaultActivation = false;

        private void Awake()
        {
            if (startupDefaultActivation)
            {
                ShowCursor();
            }
            else
            {
                HideCursor();
            }
        }

        public void ShowCursor()
        {
            IsCursorActive = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void HideCursor()
        {
            IsCursorActive = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}