using Game.Production.Model;

namespace Game.Production.Command
{
    internal class InstructionSellCraftItem : IInstruction, IExecutorInstruction
    {
        public struct Ctx
        {
            public CraftItem sellingItem;
        }

        private readonly Ctx _ctx;

        public InstructionSellCraftItem(Ctx ctx)
        {
            _ctx = ctx;
        }

        public void Apply(Logic.Logic logic)
        {
            if (_ctx.sellingItem == null ||
                !logic.inventory.EnoughCraftItem(_ctx.sellingItem.Id, _ctx.sellingItem.Count))
                return;
            foreach (var cost in _ctx.sellingItem.SellingCost)
            {
                logic.bank.IncreaseCurrency(cost);  
            }
            logic.inventory.DecreaseCraftItem(_ctx.sellingItem.Id, _ctx.sellingItem.Count);
        }
    } 
}

