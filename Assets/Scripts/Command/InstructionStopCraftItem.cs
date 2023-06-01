using Game.Production.Model;

namespace Game.Production.Command
{
    internal class InstructionStopCraftItem : IInstruction, IExecutorInstruction
    {
        public struct Ctx
        {
            public CraftItem craftItem;
            public bool isForceStop;
            public string idBuilding;
        }

        private readonly Ctx _ctx;

        public InstructionStopCraftItem(Ctx ctx)
        {
            _ctx = ctx;
        }
        
        public void Apply(Logic.Logic logic)
        {
            if (_ctx.isForceStop)
                logic.craftItem.StopCraft(_ctx.idBuilding);
            else
            {
                if(_ctx.craftItem == null)
                    return;
                Receipt receipt = logic.craftItem.GetReceipt(_ctx.craftItem);
                foreach (var cost in receipt.CostCraft)
                {
                    if(!logic.inventory.EnoughResource(cost.Id, cost.Count))
                    {
                        logic.craftItem.StopCraft(_ctx.idBuilding);
                        return;
                    }
                }

                foreach (var cost in receipt.CostCraft)
                {
                    logic.inventory.DecreaseResource(cost.Id, cost.Count);
                }
                logic.craftItem.StartCraft(_ctx.idBuilding, _ctx.craftItem);
            }
        }
    }  
}

