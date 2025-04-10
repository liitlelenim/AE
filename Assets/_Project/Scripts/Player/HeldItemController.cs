using System;
using AE.Item;
using AE.Manager;
using AE.Manager.Locator;
using DG.Tweening;
using UnityEngine;

namespace AE.Player
{
    public class HeldItemController : MonoBehaviour
    {
        [Header("Held Item Movement Settings")] 
        [SerializeField] private float itemPickUpDuration = 0.25f;

        [Header("References")] 
        [SerializeField] private Transform itemHolder;
        [SerializeField] private PickUpItem heldItem;

        private PickUpItemManager _pickUpItemManager;
        private Sequence _pickUpSequence;

        private void Awake()
        {
            _pickUpItemManager = ManagersLocator.Instance.GetManager<PickUpItemManager>();
        }

        private void OnEnable()
        {
            _pickUpItemManager.OnPickUpItem += PickUp;            
        }

        private void OnDisable()
        {
            _pickUpItemManager.OnPickUpItem -= PickUp;            
        }

        private void PickUp(PickUpItem item)
        {
            if (item == null)
            {
                Debug.LogError("Tried to pass null item to pick up method", this);
                return;
            }
            
            heldItem = item;
            heldItem.MarkAsPickedUp();
            heldItem.transform.SetParent(itemHolder, true);
            MoveHeldItemToHand();
        }

        private void PutDown()
        {
            heldItem?.MarkAsPutDown();
            heldItem?.transform.SetParent(null, true);
            _pickUpSequence?.Kill();
            heldItem = null;
        }

        private void MoveHeldItemToHand()
        {
            if (heldItem == null) return;

            _pickUpSequence?.Kill();
            _pickUpSequence = DOTween.Sequence();
            _pickUpSequence.Insert(0,
                heldItem.transform.DOLocalMove(heldItem.HoldingOffset, itemPickUpDuration)
                    .SetEase(Ease.InOutSine));

            _pickUpSequence.Insert(0,
                heldItem.transform.DOLocalRotate(heldItem.HoldingRotation, itemPickUpDuration)
                    .SetEase(Ease.InOutSine));

            _pickUpSequence.Play();
        }
    }
}