using AE.Item;
using AE.Manager;
using AE.Manager.Locator;
using UnityEngine;

namespace AE.Environment.Interactable
{
    public class OpenChestInteraction : MonoBehaviour, IInteractableObject
    {
        public bool IsAvailable => _pickUpItemManager?.CurrentHeldItem?.ItemIdentifier != null 
                                   && _pickUpItemManager.CurrentHeldItem.ItemIdentifier == keyItemIdentifier
                                   && !_alreadyInteracted;

        [Header("Interaction References")]
        [field: SerializeField]public InteractionTooltip Tooltip { get; set; }
        [SerializeField] private ItemIdentifier keyItemIdentifier;
        [SerializeField] private ChestController chestController;
        
        private PickUpItemManager _pickUpItemManager;
        private bool _alreadyInteracted;
        private void Awake()
        {
            _pickUpItemManager = ManagersLocator.Instance.GetManager<PickUpItemManager>();
        }

        public void Interact()
        {
            if (!IsAvailable) return;

            _alreadyInteracted = true;

            GameObject keyToAnimate = _pickUpItemManager.CurrentHeldItem.gameObject;
            _pickUpItemManager.PutDownItem();
            chestController.OpenChest(keyToAnimate);
        }
    }
}
