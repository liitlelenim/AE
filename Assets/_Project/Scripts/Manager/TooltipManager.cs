using System;
using AE.Environment.Interactable;
using AE.Manager.Locator;
using UnityEngine;

namespace AE.Manager
{
    public class TooltipManager : MonoBehaviour, IManager
    {
        public event Action<InteractionTooltip,bool> OnTooltipChanged;
        public bool IsInteractionAvailable { get; private set; }
        public InteractionTooltip CurrentInteractionTooltip { get; private set; }

        public void SetCurrentTooltip(InteractionTooltip tooltip, bool isAvailable)
        {
            if(CurrentInteractionTooltip == tooltip && IsInteractionAvailable == isAvailable) return;
            
            CurrentInteractionTooltip = tooltip;
            IsInteractionAvailable = isAvailable;
            
            OnTooltipChanged?.Invoke(CurrentInteractionTooltip, isAvailable);
        }
    }
}