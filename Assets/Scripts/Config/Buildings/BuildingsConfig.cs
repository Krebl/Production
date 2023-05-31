using System.Collections.Generic;
using UnityEngine;

namespace Game.Production.Config
{
    [CreateAssetMenu(
        fileName = "BuildingsConfig",
        menuName = "[Production]/Config/Buildings")]
    internal class BuildingsConfig : ScriptableObject
    {
        public List<BuildingProductionContent> GameResourceBuilding;
        [Space(10)] 
        public List<BuildingProductionContent> CrafterItemBuilding;
        [Space(10)] 
        public List<BuildingContent> Markets;
    } 
}