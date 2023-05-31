using System.Collections.Generic;
using Game.Production.Config;
using Game.Production.Model;

namespace Game.Production.Converters
{
    internal class ConverterToProductionBuildings : IConverterToProductionBuildings
    {
        public List<ProductionBuilding> ConvertToList(List<BuildingProductionContent> list)
        {
            List<ProductionBuilding> result = new List<ProductionBuilding>();
            foreach (var content in list)
            {
                result.Add(ConvertToBuilding(content));
            }

            return result;
        }

        public ProductionBuilding ConvertToBuilding(BuildingProductionContent content)
        {
            return new ProductionBuilding
            {
                Id = content.Id,
                PrefabPath = content.PrefabPath,
                Name = content.Name,
                SecondsProduction = content.SecondsProduction
            };
        }
    }
}

