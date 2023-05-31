using System.Collections.Generic;
using UnityEngine;

namespace Game.Production.Config
{
    [CreateAssetMenu(
        fileName = "GameResourcesConfig",
        menuName = "[Production]/Config/GameResources")]
    internal class GameResourceConfig : ScriptableObject
    {
        public List<ContentWithIcon> AvailableGameResources;
    }
}