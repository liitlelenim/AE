using AE.Item;
using AE.Manager;
using AE.Manager.Locator;
using UnityEngine;

namespace AE.Environment.Interactable
{
    public class PickUpInteraction : MonoBehaviour, IInteractableObject
    {
        public virtual bool IsAvailable => !_pickUpItemManager?.IsHoldingItem ?? false;

        [Header("Interaction Settings")]
        [field: SerializeField] public InteractionTooltip Tooltip { get; set; }
        [SerializeField] private PickUpItem pickUpItem;
        [SerializeField] private bool oneUsePickup;

        private PickUpItemManager _pickUpItemManager;

        private void Awake()
        {
            _pickUpItemManager = ManagersLocator.Instance.GetManager<PickUpItemManager>();
        }

        public virtual void Interact()
        {
            if (!IsAvailable) return;

            _pickUpItemManager.PickUpItem(pickUpItem);
            if(oneUsePickup) Destroy(this);
        }
    }
}