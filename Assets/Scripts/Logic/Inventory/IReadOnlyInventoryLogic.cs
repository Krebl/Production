using UniRx;
using Game.Production.Model;

namespace Game.Production.Logic
{
    internal interface IReadOnlyInventoryLogic
    {
        bool ContainResource(string id);
        bool ContainCraftItem(string id);
        IReadOnlyReactiveDictionary<string, EntityWithCount> Resources { get; }
        IReadOnlyReactiveDictionary<string, CraftItem> CraftItems { get; }

        bool EnoughCraftItem(string id, int count);

        bool EnoughResource(string id, int count);
    }
}

