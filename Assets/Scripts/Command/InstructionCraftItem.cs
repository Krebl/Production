using Game.Production.Model;

namespace Game.Production.Command
{
    internal class InstructionCraftItem : IInstruction, IExecutorInstruction
    {
        public struct Ctx
        {
            public string idBuilding;
            public CraftItem craftItem;
        }

        private readonly Ctx _ctx;

        public InstructionCraftItem(Ctx ctx)
        {
            _ctx = ctx;
        }
        
        public void Apply(Logic.Logic logic)
        {
            if(_ctx.craftItem == null)
                return;
            Receipt receipt = logic.craftItem.GetReceipt(_ctx.craftItem);
            foreach (var cost in receipt.CostCraft)
            {
                if(!logic.inventory.EnoughResource(cost.Id, cost.Count))
                    return;
            }

            foreach (var cost in receipt.CostCraft)
            {
                logic.inventory.DecreaseResource(cost.Id, cost.Count);
            }
            logic.craftItem.StartCraft(_ctx.idBuilding, _ctx.craftItem);
        }
    }
}

