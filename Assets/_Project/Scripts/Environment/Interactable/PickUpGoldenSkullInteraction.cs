using UnityEngine;

namespace AE.Environment.Interactable
{
    public class PickUpGoldenSkullInteraction : PickUpInteraction
    {
        [SerializeField] private GoldenSkullController goldenSkullController;
        public override bool IsAvailable => base.IsAvailable 
            && (goldenSkullController?.IsReady ?? true);

        public override void Interact()
        {
            base.Interact();
            goldenSkullController.MarkAsTaken();
        }
    }
}