using System;
using AE.Item;
using AE.Manager;
using AE.Manager.Locator;
using UnityEngine;

namespace AE.Environment.Interactable
{
    public class PickUpInteraction : MonoBehaviour, IInteractableObject
    {
        [field: SerializeField] public InteractionTooltip Tooltip { get; set; }
        public bool IsAvailable => !_pickUpItemManager?.IsHoldingItem ?? false;

        [SerializeField] private PickUpItem pickUpItem;

        private PickUpItemManager _pickUpItemManager;

        private void Awake()
        {
            _pickUpItemManager = ManagersLocator.Instance.GetManager<PickUpItemManager>();
        }

        public void Interact()
        {
            if (!IsAvailable) return;

            _pickUpItemManager.PickUpItem(pickUpItem);
        }
    }
}