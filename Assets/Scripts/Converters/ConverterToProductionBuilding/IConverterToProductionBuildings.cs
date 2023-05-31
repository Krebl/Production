using System.Collections.Generic;
using Game.Production.Config;
using Game.Production.Model;

namespace Game.Production.Converters
{
    internal interface IConverterToProductionBuildings
    {
        List<ProductionBuilding> ConvertToList(List<BuildingProductionContent> list);
        ProductionBuilding ConvertToBuilding(BuildingProductionContent content);
    }
}

