namespace AE.Environment.Interactable
{
    public interface IInteractableObject
    {
        public bool IsAvailable { get; }
        public InteractionTooltip Tooltip { get; set; }
        public void Interact();
    }
}