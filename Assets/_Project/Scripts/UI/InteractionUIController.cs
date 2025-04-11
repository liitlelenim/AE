using AE.Environment.Interactable;
using AE.Manager;
using AE.Manager.Locator;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AE.UI
{
    public class InteractionUIController : MonoBehaviour
    {
        [Header("Locomotion Dot Settings")] 
        [SerializeField] private float locomotionDotSizeChangeDuration = 0.2f;
        [SerializeField] private float locomotionDotEnlargedSize = 2f;

        [Header("References")]
        [SerializeField] private TextMeshProUGUI tooltipTMP;

        [SerializeField] private RawImage locomotionDot;
        private TooltipManager _tooltipManager;
        private Tween _locomotionDotTween;

        private void Awake()
        {
            _tooltipManager = ManagersLocator.Instance.GetManager<TooltipManager>();
        }

        private void OnEnable()
        {
            _tooltipManager.OnTooltipChanged += OnTooltipChange;
        }

        private void OnDisable()
        {
            _tooltipManager.OnTooltipChanged -= OnTooltipChange;
        }

        private void OnTooltipChange(InteractionTooltip tooltip, bool isInteractionAvailable)
        {
            HandleTooltipText(tooltip, isInteractionAvailable);
            HandleLocomotionDot(tooltip != null && isInteractionAvailable);
        }

        private void HandleLocomotionDot(bool shouldBeEnlarged)
        {
            _locomotionDotTween?.Kill();
            _locomotionDotTween = locomotionDot.transform
                .DOScale(shouldBeEnlarged ? Vector3.one * locomotionDotEnlargedSize : Vector3.one,
                    locomotionDotSizeChangeDuration);
            _locomotionDotTween.Play();
        }

        private void HandleTooltipText(InteractionTooltip tooltip, bool isInteractionAvailable)
        {
            string extractedText;
            if (tooltip == null)
            {
                extractedText = string.Empty;
            }
            else
            {
                extractedText = isInteractionAvailable ? tooltip.AvailableMessage : tooltip.UnavailableMessage;
            }

            tooltipTMP.text = extractedText;
        }
    }
}