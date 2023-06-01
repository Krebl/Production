using Game.Production.Model;

namespace Game.Production.Command
{
    internal class InstructionProductionResource : IInstruction, IExecutorInstruction
    {
        public struct Ctx
        {
            public string idBuilding;
            public EntityWithCount resource;
        }

        private readonly Ctx _ctx;

        public InstructionProductionResource(Ctx ctx)
        {
            _ctx = ctx;
        }
        
        public void Apply(Logic.Logic logic)
        {
            if(string.IsNullOrEmpty(_ctx.idBuilding))
                return;
            logic.production.StartProduction(_ctx.idBuilding, _ctx.resource);
        }
    }
}

