using UnityEngine;

namespace AE.Item
{
    [RequireComponent(typeof(Collider))]
    public class PickUpItem : MonoBehaviour
    {
        [Header("Holding Offset Settings")]
        [field: SerializeField] public Vector3 HoldingOffset { get; private set; }
        [field: SerializeField] public Vector3 HoldingRotation { get; private set; }

        [Header("References")]
        [SerializeField, Tooltip("Should be set only when you want rigidbody to be affected by picking up logic")]
        private Rigidbody itemRigidbody;
        
        private const string DefaultLayer = "Default";
        private const string HeldItemLayer = "HeldItem";
        
        public void MarkAsPickedUp()
        {
            DisablePhysics();
            SetHeldItemLayer();
        }

        public void MarkAsPutDown()
        {
            EnablePhysics();
            SetDefaultLayer();
        }
        
        private void EnablePhysics()
        {
            if (itemRigidbody == null) return;

            itemRigidbody.isKinematic = false;
        }

        private void DisablePhysics()
        {
            if (itemRigidbody == null) return;

            itemRigidbody.isKinematic = true;
        }

        private void SetDefaultLayer()
        {
            gameObject.layer = LayerMask.NameToLayer(DefaultLayer);
        }

        private void SetHeldItemLayer()
        {
            gameObject.layer = LayerMask.NameToLayer(HeldItemLayer);
        }
    }
}