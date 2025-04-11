using System;
using AE.Environment.Interactable;
using AE.Manager;
using AE.Manager.Locator;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AE.Player
{
    public class InteractionController : MonoBehaviour
    {
        [Header("Interaction Detection Settings")] 
        
        [SerializeField] private Transform interactionRaycastOrigin;
        [SerializeField] private float interactionDetectionDistance = 5f;
        [SerializeField] private LayerMask detectionLayerMask;

        private TooltipManager _tooltipManager;
        private IInteractableObject _detectedInteractable;
        private PlayerInputActions _playerInputActions;

        private void Awake()
        {
            _tooltipManager = ManagersLocator.Instance.GetManager<TooltipManager>();
        }

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
            bool didHit = Physics.Raycast(interactionRaycastOrigin.transform.position,
                interactionRaycastOrigin.forward,
                out RaycastHit hitResult,
                interactionDetectionDistance,
                detectionLayerMask);
           
            if (didHit && hitResult.transform.TryGetComponent(out IInteractableObject interactable))
            {
                _detectedInteractable = interactable;
                _tooltipManager.SetCurrentTooltip(interactable.Tooltip, interactable.IsAvailable);
                return;
            }
            
            _tooltipManager.SetCurrentTooltip(null, false);
            _detectedInteractable = null;
        }

        private void InitializePlayerInputActions()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Interaction.Interact.performed += OnInteractButtonDown;
        }
    }
}