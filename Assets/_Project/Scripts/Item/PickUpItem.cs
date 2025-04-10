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
        [SerializeField, Tooltip("Should be set only when you want rigidbody to be affected by picking up logic")] private Rigidbody itemRigidbody;

        private Collider _collider;
        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public void MarkAsPickedUp()
        {
            DisablePhysics();
        }

        public void MarkAsPutDown()
        {
            EnablePhysics();
        }


        private void EnablePhysics()
        {
            if (itemRigidbody == null) return;

            _collider.enabled = true;
            itemRigidbody.isKinematic = false;
        }

        private void DisablePhysics()
        {
            if (itemRigidbody == null) return;

            _collider.enabled = false;
            itemRigidbody.isKinematic = true;
        }
        
    }
}