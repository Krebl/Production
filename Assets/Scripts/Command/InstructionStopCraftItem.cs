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
                logic.craftItem.StartCraft(_ctx.idBuilding, _ctx.craftItem);
                logic.inventory.AddCraftItem(_ctx.craftItem);
            }
        }
    }  
}

