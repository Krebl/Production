using System.Collections.Generic;
using Game.Production.Config;
using Game.Production.Model;


namespace Game.Production.Converters
{
    internal interface IConverterToCraftItem
    {
        List<CraftItem> ConvertToList(List<CraftItemContent> list);
        CraftItem ConvertToItem(CraftItemContent content);
    }
}

