
namespace Game.Production.Logic
{
    internal class Logic : IReadOnlyLogic
    {
        public IReadOnlyInventoryLogic Inventory => inventory;
        public IReadOnlyBankLogic Bank => bank;
        public IWinLogic Win => win;

        public InventoryLogic inventory;
        public BankLogic bank;
        public WinLogic win;
        public ProductionLogic production;
    }
}

