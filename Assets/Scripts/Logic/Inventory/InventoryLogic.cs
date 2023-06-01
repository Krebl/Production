using UniRx;
using Game.Production.Model;
using Game.Production.Tools;

namespace Game.Production.Logic
{
    internal class InventoryLogic : BaseDisposable, IReadOnlyInventoryLogic
    {
        public struct Ctx
        {
            
        }

        private readonly Ctx _ctx;

        public InventoryLogic(Ctx ctx)
        {
            _resources = new ReactiveDictionary<string, EntityWithCount>();
            _craftItems = new ReactiveDictionary<string, CraftItem>();
            _ctx = ctx;
        }
        
        private ReactiveDictionary<string, EntityWithCount> _resources;
        private ReactiveDictionary<string, CraftItem> _craftItems;


        public bool ContainResource(string id)
        {
            return !string.IsNullOrEmpty(id) && _resources.ContainsKey(id);   
        }

        public bool ContainCraftItem(string id)
        {
            return !string.IsNullOrEmpty(id) && _craftItems.ContainsKey(id);
        }

        public bool EnoughCraftItem(string id, int count)
        {
            return ContainCraftItem(id) && _craftItems[id].Count >= count;
        }
        
        public bool EnoughResource(string id, int count)
        {
            return ContainResource(id) && _resources[id].Count >= count;
        }

        public IReadOnlyReactiveDictionary<string, EntityWithCount> Resources => _resources;
        public IReadOnlyReactiveDictionary<string, CraftItem> CraftItems => _craftItems;

        public void AddResource(EntityWithCount resource)
        {
            if(resource == null)
                return;
            _resources[resource.Id] = resource;
        }

        public void AddCraftItem(CraftItem item)
        {
            if(item == null)
                return;
            _craftItems[item.Id] = item;
        }

        public void DecreaseResource(string id, int count)
        {
            if(!ContainResource(id))
                return;

            EntityWithCount resource = _resources[id];
            if(resource.Count < count)
                return;
            
            resource.Count -= count;
            if (resource.Count > 0)
                _resources[id] = resource;
            else
                _resources.Remove(id);
        }
        
        public void DecreaseCraftItem(string id, int count)
        {
            if(!ContainCraftItem(id))
                return;

            CraftItem item = _craftItems[id];
            if(item.Count < count)
                return;
            
            item.Count -= count;
            if (item.Count > 0)
                _craftItems[id] = item;
            else
                _craftItems.Remove(id);
        }
    }
}