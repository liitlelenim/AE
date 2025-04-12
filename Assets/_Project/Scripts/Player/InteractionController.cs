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
        private InputManager _inputManager;
        private IInteractableObject _detectedInteractable;
        private PlayerInputActions _playerInputActions;

        private void Awake()
        {
            _tooltipManager = ManagersLocator.Instance.GetManager<TooltipManager>();
            _inputManager = ManagersLocator.Instance.GetManager<InputManager>();
        }

        private void OnEnable()
        {
            _inputManager.OnInteractionPerformed += TryToInteract;
        }

        private void OnDisable()
        {
            _inputManager.OnInteractionPerformed -= TryToInteract;
        }

        private void Update()
        {
            TryToDetectInteractable();
        }

        private void TryToInteract()
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
        
    }
}