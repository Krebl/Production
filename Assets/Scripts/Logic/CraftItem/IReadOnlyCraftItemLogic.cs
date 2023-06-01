using Game.Production.Model;
using UniRx;


namespace Game.Production.Logic
{
    internal interface IReadOnlyCraftItemLogic
    {
        Receipt GetReceipt(EntityWithCount[] ingredients);
        Receipt GetReceipt(CraftItem craftItem);
        IReadOnlyReactiveDictionary<string, ReactiveProperty<int>> Timers { get; }
        IReadOnlyReactiveDictionary<string, CraftItem> CurrentCraftingItem { get; }
    }
}

