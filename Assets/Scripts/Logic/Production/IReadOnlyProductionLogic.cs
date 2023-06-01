using Game.Production.Model;
using UniRx;

namespace Game.Production.Logic
{
    internal interface IReadOnlyProductionLogic
    {
        IReadOnlyReactiveDictionary<string, ReactiveProperty<int>> Timers { get; }
        IReadOnlyReactiveDictionary<string, EntityWithCount> CurrentProductionResource { get; }
    }
}

