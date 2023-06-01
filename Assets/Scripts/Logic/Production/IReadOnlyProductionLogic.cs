using UniRx;

namespace Game.Production.Logic
{
    internal interface IReadOnlyProductionLogic
    {
        IReadOnlyReactiveDictionary<string, ReactiveProperty<int>> Timers { get; }
    }
}

