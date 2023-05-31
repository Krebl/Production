using System.Collections.Generic;
using UnityEngine;

namespace Game.Production.Config
{
    [CreateAssetMenu(
        fileName = "CraftItemsConfig",
        menuName = "[Production]/Config/CraftItems")]
    internal class CraftItemsConfig : ScriptableObject
    {
        public List<CraftItemContent> AvailableItems;
    }
}

