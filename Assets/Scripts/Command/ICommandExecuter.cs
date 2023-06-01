

namespace Game.Production.Command
{
    internal interface ICommandExecuter
    {
        void Execute(IInstruction instruction);
    }
}

