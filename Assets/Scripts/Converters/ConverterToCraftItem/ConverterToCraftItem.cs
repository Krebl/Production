using System.Collections.Generic;
using Game.Production.Config;
using Game.Production.Model;

namespace Game.Production.Converters
{
    internal class ConverterToCraftItem : IConverterToCraftItem
    {
        public struct Ctx
        {
            public IConverterToEntityWithCount converterToEntityWithCount;
        }

        private readonly Ctx _ctx;

        public ConverterToCraftItem(Ctx ctx)
        {
            _ctx = ctx;
        }
        
        public List<CraftItem> ConvertToList(List<CraftItemContent> list)
        {
            List<CraftItem> result = new List<CraftItem>();
            foreach (var content in list)
            {
                result.Add(ConvertToItem(content));
            }

            return result;
        }
        
        public CraftItem ConvertToItem(CraftItemContent content)
        {
            return new CraftItem
            {
                Id = content.Id,
                Name = content.Name,
                IconPath = content.IconPath,
                Count = 0,
                SellingCost = _ctx.converterToEntityWithCount.ConvertToList(content.SellingCost)
            };
        }
    }
}

