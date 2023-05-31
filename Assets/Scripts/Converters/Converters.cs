using Game.Production.Config;
using Game.Production.Model;

namespace Game.Production.Converters
{
    internal class Converters
    {
        public struct Ctx
        {
            public GameConfig gameConfig;
        }

        private readonly Ctx _ctx;
        
        public Converters(Ctx ctx)
        {
            _ctx = ctx;
            _converterToEntityWithCount = new ConverterToEntityWithCount(new ConverterToEntityWithCount.Ctx
            {
                resource = _ctx.gameConfig.GameResource.AvailableGameResources,
                currency = _ctx.gameConfig.Currency.AvailableCurrency
            });
            _converterToCraftItem = new ConverterToCraftItem(new ConverterToCraftItem.Ctx
            {
                converterToEntityWithCount = _converterToEntityWithCount
            });
            _converterToReceipt = new ConverterToReceipt(new ConverterToReceipt.Ctx
            {
                craftItems = _ctx.gameConfig.CraftItems.AvailableItems,
                converterToCraftItem = _converterToCraftItem,
                converterToEntityWithCount = _converterToEntityWithCount,
            });
            _converterToBuildings = new ConverterToBuildings();
            _converterToProductionBuildings = new ConverterToProductionBuildings();
            _converterConfigToHub = new ConverterConfigToHub(new ConverterConfigToHub.Ctx
            {
                converterToEntityWithCount = _converterToEntityWithCount,
                converterToCraftItem = _converterToCraftItem,
                converterToReceipt = _converterToReceipt,
                converterToBuildings = _converterToBuildings,
                converterToProductionBuildings = _converterToProductionBuildings
            });
        }

        private ConverterConfigToHub _converterConfigToHub;
        private IConverterToEntityWithCount _converterToEntityWithCount;
        private IConverterToCraftItem _converterToCraftItem;
        private IConverterToReceipt _converterToReceipt;
        private IConverterToBuildings _converterToBuildings;
        private IConverterToProductionBuildings _converterToProductionBuildings;

        public Hub ConvertToHub()
        {
           return _converterConfigToHub.Convert(_ctx.gameConfig);
        }

        public EntityWithCount ConvertWinCondition()
        {
            return _converterToEntityWithCount.ConvertToEntity(_ctx.gameConfig.CurrencyForWin);
        }
    }
}

