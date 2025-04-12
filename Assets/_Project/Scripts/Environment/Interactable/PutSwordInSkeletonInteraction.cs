using AE.Item;
using AE.Manager;
using AE.Manager.Locator;
using DG.Tweening;
using UnityEngine;

namespace AE.Environment.Interactable
{
    public class PutSwordInSkeletonInteraction : MonoBehaviour, IInteractableObject
    {
        public bool IsAvailable =>
            (_pickUpItemManager?.CurrentHeldItem?.ItemIdentifier ?? false) == swordItemIdentifier
            && !_alreadyInteracted;

        [Header("Interaction Settings")]
        [field: SerializeField]
        public InteractionTooltip Tooltip { get; set; }

        [SerializeField] private ItemIdentifier swordItemIdentifier;

        [Header("Interaction Animation Settings")] [SerializeField]
        private GameObject afterInteractionEffectsParent;

        [SerializeField] private Transform swordTweenStartingTransform;
        [SerializeField] private Transform swordTweenEndPosition;
        [SerializeField] private float swordSetUpDuration = 1f;
        [SerializeField] private float swordThrustDuration = 0.2f;

        [Header("References")] [SerializeField] private SkeletonsAndSwordsPuzzleController puzzleController;
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
            CleanUpSwordObject();

            PlaySwordAnimationSequence();
        }

        private void CleanUpSwordObject()
        {
            Tooltip = null;
            _tooltipManager.SetCurrentTooltip(null, false);
            Destroy(_usedSwordGameObject.GetComponent<PickUpInteraction>());
        }

        private void PlaySwordAnimationSequence()
        {
            Sequence interactionAnimationSequence = DOTween.Sequence();

            interactionAnimationSequence.Append(
                _usedSwordGameObject.transform.DOMove(swordTweenStartingTransform.position, swordSetUpDuration));
            interactionAnimationSequence.Join(
                _usedSwordGameObject.transform.DORotate(swordTweenStartingTransform.eulerAngles, swordSetUpDuration));
            interactionAnimationSequence.Append(
                _usedSwordGameObject.transform.DOMove(swordTweenEndPosition.position, swordThrustDuration));
            interactionAnimationSequence.OnComplete(OnInteractionAnimationComplete);
            interactionAnimationSequence.SetAutoKill();
            interactionAnimationSequence.Play();
        }

        private void OnInteractionAnimationComplete()
        {
            afterInteractionEffectsParent?.gameObject.SetActive(true);
            puzzleController?.MarkPuzzleProgress();
        }
    }
}