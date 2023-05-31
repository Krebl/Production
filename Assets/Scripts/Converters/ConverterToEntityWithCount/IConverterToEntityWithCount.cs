using System.Collections.Generic;
using Game.Production.Config;
using Game.Production.Model;

namespace Game.Production.Converters
{
    internal interface IConverterToEntityWithCount
    {
        List<EntityWithCount> ConvertToList(List<ContentWithIcon> list);
        List<EntityWithCount> ConvertToList(CostContent[] list);
        EntityWithCount ConvertToEntity(ContentWithIcon content);
        EntityWithCount ConvertToEntity(CostContent content);
    } 
}

