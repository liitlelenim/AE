using System;
using AE.Manager;
using AE.Manager.Locator;
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

        private InputManager _inputManager;
        
        private Vector2 _currentLookAngles;
        private Vector2 _lookInput;

        private void Awake()
        {
            _inputManager = ManagersLocator.Instance.GetManager<InputManager>();
        }

        private void Update()
        {
            ReadInput();
            UpdateLook();
        }

        private void ReadInput()
        {
            _lookInput = _inputManager.LookInput;
        }

        private void UpdateLook()
        {
            _currentLookAngles.x += _lookInput.x * xLookSensitivity * Time.deltaTime;
            _currentLookAngles.y += -_lookInput.y * yLookSensitivity * Time.deltaTime;

            _currentLookAngles.y = Mathf.Clamp(_currentLookAngles.y, minVerticalAngle, maxVerticalAngle);

            horizontalRotationParent.localRotation = Quaternion.Euler(Vector3.up * _currentLookAngles.x);
            verticalRotationParent.localRotation = Quaternion.Euler(Vector3.right * _currentLookAngles.y);
        }
        
    }
}