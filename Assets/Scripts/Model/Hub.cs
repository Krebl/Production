using System.Collections.Generic;

namespace Game.Production.Model
{
    internal class Hub
    {
        public IReadOnlyList<EntityWithCount> availableResource;
        public IReadOnlyList<EntityWithCount> availableCurrency;
        public IReadOnlyList<CraftItem> availableItems;
        public IReadOnlyList<Receipt> availableReceipts;
        public IReadOnlyList<ProductionBuilding> productionResourceBuildings;
        public IReadOnlyList<ProductionBuilding> craftItemBuildings;
        public IReadOnlyList<Building> markets;
    }
}

