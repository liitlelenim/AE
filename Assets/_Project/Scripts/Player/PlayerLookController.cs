using UnityEngine;
using UnityEngine.InputSystem;

namespace AE.Player
{
    public class PlayerLookController : MonoBehaviour
    {
        [Header("Rotation Axes Parents")]
        [SerializeField] private Transform horizontalRotationParent;
        [SerializeField] private Transform verticalRotationParent;

        [Header("Look Settings")] 
        [SerializeField] private float xLookSensitivity = 90f;
        [SerializeField] private float yLookSensitivity = 90f;
        [SerializeField] private float minVerticalAngle = -60f;
        [SerializeField] private float maxVerticalAngle = 60f;

        private PlayerInputActions _playerInputActions;

        private Vector2 _currentLookAngles;
        private Vector2 _lookInput;

        private void OnEnable()
        {
            if (_playerInputActions == null)
            {
                InitializePlayerInputActions();
            }

            _playerInputActions?.Enable();
        }

        private void OnDisable()
        {
            _playerInputActions?.Disable();
        }

        private void Update()
        {
            UpdateLook();
        }

        private void UpdateLook()
        {
            _currentLookAngles.x += _lookInput.x * xLookSensitivity * Time.deltaTime;
            _currentLookAngles.y += -_lookInput.y * yLookSensitivity * Time.deltaTime;

            _currentLookAngles.y = Mathf.Clamp(_currentLookAngles.y, minVerticalAngle, maxVerticalAngle);

            horizontalRotationParent.localRotation = Quaternion.Euler(Vector3.up * _currentLookAngles.x);
            verticalRotationParent.localRotation = Quaternion.Euler(Vector3.right * _currentLookAngles.y);
        }

        private void HandleLookInput(InputAction.CallbackContext context)
        {
            _lookInput = context.ReadValue<Vector2>();
        }

        private void InitializePlayerInputActions()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Navigation.Look.performed += HandleLookInput;
            _playerInputActions.Navigation.Look.canceled += HandleLookInput;
        }
    }
}