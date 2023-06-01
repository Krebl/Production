using System.Collections.Generic;
using System.Linq;
using Game.Production.Model;
using Game.Production.Tools;
using UniRx;

namespace Game.Production.Logic
{
    internal class CraftItemLogic : BaseDisposable, IReadOnlyCraftItemLogic
    {
        public struct Ctx
        {
            public IReadOnlyList<Receipt> receipts;
            public IReadOnlyList<ProductionBuilding> buildings;
        }

        private readonly Ctx _ctx;
        private ReactiveDictionary<string, ReactiveProperty<int>> _timers;
        private ReactiveDictionary<string, CraftItem> _currentCraftingItem;

        public CraftItemLogic(Ctx ctx)
        {
            _timers = new ReactiveDictionary<string, ReactiveProperty<int>>();
            _currentCraftingItem = new ReactiveDictionary<string, CraftItem>();
            TimersTools timersTools = new TimersTools(new TimersTools.Ctx
            {
                timers = _timers
            });
            AddDispose(timersTools);
            _ctx = ctx;
        }

        public bool CanCraft(EntityWithCount[] ingredients)
        {
            bool found = false;
            foreach (var receipt in _ctx.receipts)
            {
                for (int i = 0; i < ingredients.Length; i++)
                {
                    if (!receipt.CostCraft.Exists(cost => cost.Id == ingredients[i].Id))
                    {
                        found = false;
                        break;
                    }
                    else
                        found = true;
                }

                if (found)
                    return true;
            }

            return false;
        }

        public IReadOnlyReactiveDictionary<string, ReactiveProperty<int>> Timers => _timers;
        public IReadOnlyReactiveDictionary<string, CraftItem> CurrentCraftingItem => _currentCraftingItem;

        public void StartCraft(string idBuilding, CraftItem craftItem)
        {
            ProductionBuilding productionBuilding = _ctx.buildings.FirstOrDefault(building => building.Id == idBuilding);
            if(productionBuilding == default)
                return;
            if (_timers.TryGetValue(idBuilding, out ReactiveProperty<int> timer))
                timer.Value = productionBuilding.SecondsProduction;
            else
                _timers[idBuilding] = new ReactiveProperty<int>(productionBuilding.SecondsProduction);
            _currentCraftingItem[idBuilding] = craftItem;
        }

        public void StopCraft(string idBuilding)
        {
            _timers.Remove(idBuilding);
            _currentCraftingItem.Remove(idBuilding);
        }
    }
}

