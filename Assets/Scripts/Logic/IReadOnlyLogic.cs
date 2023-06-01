
namespace Game.Production.Logic
{
    internal interface IReadOnlyLogic
    {
        public IReadOnlyInventoryLogic Inventory { get; }
        public IReadOnlyBankLogic Bank { get; }
        public IWinLogic Win { get; }
        public IReadOnlyProductionLogic Production { get; }
        public IReadOnlyCraftItemLogic CraftItem { get; }
    }
}

