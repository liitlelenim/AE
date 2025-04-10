using System;
using AE.Environment.Interactable;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AE.Player
{
    public class InteractionController : MonoBehaviour
    {
        [Header("Interaction Detection Settings")] 
        
        [SerializeField] private Transform interactionRaycastOrigin;
        [SerializeField] private float interactionDetectionDistance = 5f;

        private IInteractableObject _detectedInteractable;
        private PlayerInputActions _playerInputActions;
        private readonly RaycastHit[] _raycastHitResult = new RaycastHit[10];

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
            TryToDetectInteractable();
        }

        private void OnInteractButtonDown(InputAction.CallbackContext _)
        {
            if (!(_detectedInteractable?.IsAvailable ?? false)) return;

            _detectedInteractable?.Interact();
        }

        private void TryToDetectInteractable()
        {
            int size = Physics.RaycastNonAlloc(interactionRaycastOrigin.transform.position,
                interactionRaycastOrigin.forward, _raycastHitResult, interactionDetectionDistance);

            for (int i = 0; i < size; i++)
            {
                if (_raycastHitResult[i].transform.TryGetComponent(out IInteractableObject interactable))
                {
                    _detectedInteractable = interactable;
                    return;
                }
            }

            _detectedInteractable = null;
        }

        private void InitializePlayerInputActions()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Interaction.Interact.performed += OnInteractButtonDown;
        }
    }
}