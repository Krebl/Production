using Game.Production.Config;
using Game.Production.Model;

namespace Game.Production.Converters
{
    internal class ConverterConfigToHub
    {
        public struct Ctx
        {
            public IConverterToEntityWithCount converterToEntityWithCount;
            public IConverterToCraftItem converterToCraftItem;
            public IConverterToReceipt converterToReceipt;
            public IConverterToBuildings converterToBuildings;
            public IConverterToProductionBuildings converterToProductionBuildings;
        }

        private readonly Ctx _ctx;

        public ConverterConfigToHub(Ctx ctx)
        {
            _ctx = ctx;
        }
        
        public Hub Convert(GameConfig config)
        {
            return new Hub
            {
                availableResource = _ctx.converterToEntityWithCount.ConvertToList(config.GameResource.AvailableGameResources),
                availableCurrency = _ctx.converterToEntityWithCount.ConvertToList(config.Currency.AvailableCurrency),
                availableItems = _ctx.converterToCraftItem.ConvertToList(config.CraftItems.AvailableItems),
                availableReceipts = _ctx.converterToReceipt.ConvertToList(config.Receipts.AvailableReceipts),
                markets = _ctx.converterToBuildings.ConvertToList(config.Buildings.Markets),
                productionResourceBuildings = _ctx.converterToProductionBuildings.ConvertToList(config.Buildings.GameResourceBuilding),
                craftItemBuildings = _ctx.converterToProductionBuildings.ConvertToList(config.Buildings.CrafterItemBuilding),
            };
        }
    } 
}

