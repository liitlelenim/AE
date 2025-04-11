using System;
using AE.Item;
using AE.Manager;
using AE.Manager.Locator;
using UnityEngine;

namespace AE.Environment.Interactable
{
    public class PutSwordInSkeletonInteraction : MonoBehaviour, IInteractableObject
    {
        public bool IsAvailable =>
            (_pickUpItemManager?.CurrentHeldItem?.ItemIdentifier ?? false) == swordItemIdentifier;

        [field: SerializeField] public InteractionTooltip Tooltip { get; set; }
        [SerializeField] private ItemIdentifier swordItemIdentifier;
        private PickUpItemManager _pickUpItemManager;

        private void Awake()
        {
            _pickUpItemManager = ManagersLocator.Instance.GetManager<PickUpItemManager>();
        }

        public void Interact()
        {
            Debug.Log("PutSwordInBodyInteraction Interact");
        }
    }
}