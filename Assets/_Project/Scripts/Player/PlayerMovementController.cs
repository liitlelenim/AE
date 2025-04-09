using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AE.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 7.5f;

        private PlayerInputActions _playerInputActions;
        private CharacterController _characterController;

        private Vector3 _movementDirectionInput;

        private void OnEnable()
        {
            _playerInputActions?.Enable();
        }

        private void OnDisable()
        {
            _playerInputActions?.Disable();
        }

        private void Update()
        {
            _characterController.Move(_movementDirectionInput * (movementSpeed * Time.deltaTime));
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerInputActions = new PlayerInputActions();

            _playerInputActions.Navigation.Move.performed += HandleMoveInput;
            _playerInputActions.Navigation.Move.canceled += HandleMoveInput;
        }

        private void HandleMoveInput(InputAction.CallbackContext callbackContext)
        {
            Vector2 rawDirectionInput = callbackContext.ReadValue<Vector2>();
            _movementDirectionInput = new Vector3(rawDirectionInput.x, 0, rawDirectionInput.y);
        }
    }
}