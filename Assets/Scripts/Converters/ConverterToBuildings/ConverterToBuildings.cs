using System.Collections.Generic;
using Game.Production.Config;
using Game.Production.Model;

namespace Game.Production.Converters
{
    internal class ConverterToBuildings : IConverterToBuildings
    {
        public List<Building> ConvertToList(List<BuildingContent> list)
        {
            List<Building> result = new List<Building>();
            foreach (var content in list)
            {
                result.Add(ConvertToBuilding(content));
            }

            return result;
        }

        public Building ConvertToBuilding(BuildingContent content)
        {
            return new Building
            {
                Id = content.Id,
                Name = content.Name,
                PrefabPath = content.PrefabPath
            };
        }
    }
}

