using Game.Production.Model;

namespace Game.Production.Command
{
    internal class InstructionCraftItem : IInstruction, IExecutorInstruction
    {
        public struct Ctx
        {
            public Receipt receipt;
        }

        private readonly Ctx _ctx;

        public InstructionCraftItem(Ctx ctx)
        {
            _ctx = ctx;
        }
        
        public void Apply(Logic.Logic logic)
        {
            if(_ctx.receipt == null)
                return;
            foreach (var cost in _ctx.receipt.CostCraft)
            {
                if(!logic.inventory.EnoughResource(cost.Id, cost.Count))
                    return;
            }

            foreach (var cost in _ctx.receipt.CostCraft)
            {
                logic.inventory.DecreaseResource(cost.Id, cost.Count);
            }
        }
    }
}

