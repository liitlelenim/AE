using System;
using AE.Manager.Locator;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AE.Manager
{
    public class InputManager : MonoBehaviour, IManager
    {
        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        
        public event Action OnInteractionPerformed;

        private PlayerInputActions _playerInputActions;

        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            RegisterActions();
        }

        private void OnEnable()
        {
            _playerInputActions.Enable();
        }

        private void OnDisable()
        {
            _playerInputActions.Disable();
        }

        private void RegisterActions()
        {
            _playerInputActions.Interaction.Interact.performed += HandleInteractionPerformed;
            _playerInputActions.Navigation.Move.performed += HandleMove;
            _playerInputActions.Navigation.Look.performed += HandleLook;
            
            _playerInputActions.Navigation.Move.canceled += HandleMove;
            _playerInputActions.Navigation.Look.canceled += HandleLook;
        }

        private void HandleMove(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }

        private void HandleLook(InputAction.CallbackContext context)
        {
            LookInput = context.ReadValue<Vector2>();
        }

        private void HandleInteractionPerformed(InputAction.CallbackContext context)
        {
            OnInteractionPerformed?.Invoke();
        }
    }
}