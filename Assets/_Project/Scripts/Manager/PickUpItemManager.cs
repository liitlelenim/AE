using System;
using AE.Item;
using AE.Manager.Locator;
using UnityEngine;

namespace AE.Manager
{
    public class PickUpItemManager : MonoBehaviour, IManager
    {
        public event Action<PickUpItem> OnPickUpItem;

        public void PickUpItem(PickUpItem item)
        {
            OnPickUpItem?.Invoke(item);
        }
    }
}