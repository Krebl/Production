
namespace Game.Production.Command
{
    internal class InstructionWin : IInstruction, IExecutorInstruction
    {
        public struct Ctx
        {
            
        }

        private readonly Ctx _ctx;

        public InstructionWin(Ctx ctx)
        {
            _ctx = ctx;
        }
        
        public void Apply(Logic.Logic logic)
        {
            logic.production.Clear();
            logic.craftItem.Clear();
            logic.inventory.Clear();
            logic.bank.Clear();
        }
    }
}

