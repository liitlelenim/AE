using System;
using AE.Item;
using AE.Manager.Locator;
using UnityEngine;

namespace AE.Manager
{
    public class PickUpItemManager : MonoBehaviour, IManager
    {
        public event Action<PickUpItem> OnPickUpItem;
        public PickUpItem CurrentHeldItem { get; private set; }
        public bool IsHoldingItem => CurrentHeldItem != null;

        public void PickUpItem(PickUpItem item)
        {
            if (item == null)
            {
                Debug.LogWarning("Tried to pick up null item", this);
                return;
            }

            OnPickUpItem?.Invoke(item);
            CurrentHeldItem = item;
        }

        public void PutDownItem()
        {
            CurrentHeldItem = null;
        }
    }
}