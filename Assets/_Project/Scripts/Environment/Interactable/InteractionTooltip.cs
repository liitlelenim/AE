using UnityEngine;

namespace AE.Environment.Interactable
{
    [CreateAssetMenu(menuName = "Interactable/Interaction Tooltip", fileName = "Interaction Tooltip")]
    public class InteractionTooltip : ScriptableObject
    {
        [field: SerializeField] public string AvailableMessage { get; private set; }
        [field: SerializeField] public string UnavailableMessage { get; private set; }
    }
}