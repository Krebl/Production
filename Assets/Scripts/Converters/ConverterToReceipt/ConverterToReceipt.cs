using System.Collections.Generic;
using System.Linq;
using Game.Production.Config;
using Game.Production.Model;

namespace Game.Production.Converters
{
    internal class ConverterToReceipt : IConverterToReceipt
    {
        public struct Ctx
        {
            public IConverterToEntityWithCount converterToEntityWithCount;
            public IConverterToCraftItem converterToCraftItem;
            public IReadOnlyList<CraftItemContent> craftItems;
        }

        private readonly Ctx _ctx;

        public ConverterToReceipt(Ctx ctx)
        {
            _ctx = ctx;
        }
        
        public List<Receipt> ConvertToList(List<ReceiptContent> list)
        {
            List<Receipt> result = new List<Receipt>();
            foreach (var content in list)
            {
                result.Add(ConvertToReceipt(content));
            }

            return result;
        }

        public Receipt ConvertToReceipt(ReceiptContent content)
        {
            CraftItemContent resultCraft = _ctx.craftItems.FirstOrDefault(item => item.Id == content.IdResult);
            if (resultCraft == default)
                return null;
            
            return new Receipt
            {
                Id = content.Id,
                Result = _ctx.converterToCraftItem.ConvertToItem(resultCraft),
                CostCraft = _ctx.converterToEntityWithCount.ConvertToList(content.CostCraft),
            };
        }
    } 
}

