using Game.Production.Model;

namespace Game.Production.Command
{
    internal class InstructionStopProductionResource : IInstruction, IExecutorInstruction
    {
        public struct Ctx
        {
            public EntityWithCount resource;
            public bool isForceStop;
            public string idBuilding;
        }

        private readonly Ctx _ctx;

        public InstructionStopProductionResource(Ctx ctx)
        {
            _ctx = ctx;
        }
        
        public void Apply(Logic.Logic logic)
        {
            if (_ctx.isForceStop)
                logic.production.StopProduction(_ctx.idBuilding);
            else
            {
                if(_ctx.resource == null)
                    return;
                logic.production.StartProduction(_ctx.idBuilding, _ctx.resource.Id);
                logic.inventory.AddResource(_ctx.resource);  
            }
        }
    }
}

