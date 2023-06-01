using Game.Production.Model;
using UniRx;


namespace Game.Production.Logic
{
    internal interface IReadOnlyCraftItemLogic
    {
        bool CanCraft(EntityWithCount[] ingredients);
        IReadOnlyReactiveDictionary<string, ReactiveProperty<int>> Timers { get; }
    }
}

