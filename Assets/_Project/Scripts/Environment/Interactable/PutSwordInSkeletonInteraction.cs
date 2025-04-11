using AE.Item;
using AE.Manager;
using AE.Manager.Locator;
using UnityEngine;

namespace AE.Environment.Interactable
{
    public class PutSwordInSkeletonInteraction : MonoBehaviour, IInteractableObject
    {
        public bool IsAvailable =>
            (_pickUpItemManager?.CurrentHeldItem?.ItemIdentifier ?? false) == swordItemIdentifier
            && !_alreadyInteracted;

        [field: SerializeField] public InteractionTooltip Tooltip { get; set; }
        [SerializeField] private ItemIdentifier swordItemIdentifier;
        
        private PickUpItemManager _pickUpItemManager;
        private TooltipManager _tooltipManager;
        private GameObject _usedSwordGameObject;
        private bool _alreadyInteracted = false;

        private void Awake()
        {
            _pickUpItemManager = ManagersLocator.Instance.GetManager<PickUpItemManager>();
            _tooltipManager = ManagersLocator.Instance.GetManager<TooltipManager>();
        }

        public void Interact()
        {
            if (!IsAvailable) return;

            _alreadyInteracted = true;
            _usedSwordGameObject = _pickUpItemManager.CurrentHeldItem.gameObject;
            _pickUpItemManager.PutDownItem();
            RemoveInteractivityFromUsedObjects();

            PlaySwordAnimationSequence();
        }

        private void PlaySwordAnimationSequence()
        {
            
        }

        private void RemoveInteractivityFromUsedObjects()
        {
            Tooltip = null;
            _tooltipManager.SetCurrentTooltip(null, false);
            Destroy(_usedSwordGameObject.GetComponent<PickUpInteraction>());
        }
    }
}