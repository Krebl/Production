
namespace Game.Production.Command
{
    internal class CommandExecuter : ICommandExecuter
    {
        public struct Ctx
        {
            public Logic.Logic logic;
        }

        private readonly Ctx _ctx;

        public CommandExecuter(Ctx ctx)
        {
            _ctx = ctx;
        }

        public void Execute(IInstruction instruction)
        {
            if(instruction is IExecutorInstruction executorInstruction)
                executorInstruction.Apply(_ctx.logic);
        }
    }
}

