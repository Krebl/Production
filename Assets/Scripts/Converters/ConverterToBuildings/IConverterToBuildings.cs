using System.Collections.Generic;
using Game.Production.Config;
using Game.Production.Model;

namespace Game.Production.Converters
{
    internal interface IConverterToBuildings
    {
        List<Building> ConvertToList(List<BuildingContent> list);
        Building ConvertToBuilding(BuildingContent content);
    }
}

