using System;
using UnityEngine;

namespace AE.Item
{
    [CreateAssetMenu(menuName = "Item/Item Identifier", fileName = "Item Identifier")]
    public class ItemIdentifier : ScriptableObject
    {
        public Guid ItemGuid { get; private set; } = Guid.NewGuid();
    }
}