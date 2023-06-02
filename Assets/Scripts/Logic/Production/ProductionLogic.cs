using System.Collections.Generic;
using System.Linq;
using Game.Production.Tools;
using Game.Production.Model;
using UniRx;

namespace Game.Production.Logic
{
    internal class ProductionLogic : BaseDisposable, IReadOnlyProductionLogic
    {
        public struct Ctx
        {
            public IReadOnlyList<ProductionBuilding> buildings;
        }

        private Ctx _ctx;
        private readonly ReactiveDictionary<string, ReactiveProperty<int>> _timers;
        private readonly ReactiveDictionary<string, EntityWithCount> _currentProductionResource;
        
        public ProductionLogic(Ctx ctx)
        {
            _timers = new ReactiveDictionary<string, ReactiveProperty<int>>();
            _currentProductionResource = new ReactiveDictionary<string, EntityWithCount>();
            _ctx = ctx;
            TimersTools timersTools = new TimersTools(new TimersTools.Ctx
            {
                timers = _timers
            });
            AddDispose(timersTools);
        }

        public void StartProduction(string idBuilding, EntityWithCount resource)
        {
            ProductionBuilding productionBuilding = _ctx.buildings.FirstOrDefault(building => building.Id == idBuilding);
            if(productionBuilding == default)
                return;
            if (_timers.TryGetValue(idBuilding, out ReactiveProperty<int> timer))
                timer.Value = productionBuilding.SecondsProduction;
            else
                _timers[idBuilding] = new ReactiveProperty<int>(productionBuilding.SecondsProduction);
            _currentProductionResource[idBuilding] = resource;
        }

        public void StopProduction(string idBuilding)
        {
            _timers.Remove(idBuilding);
            _currentProductionResource.Remove(idBuilding);
        }

        public IReadOnlyReactiveDictionary<string, ReactiveProperty<int>> Timers => _timers;
        public IReadOnlyReactiveDictionary<string, EntityWithCount> CurrentProductionResource => _currentProductionResource;

        public void Clear()
        {
            List<string> keysBuilding = _currentProductionResource.Keys.ToList();
            foreach (var key in keysBuilding)
            {
                _currentProductionResource.Remove(key);
            }

            keysBuilding = _timers.Keys.ToList();
            foreach (var key in keysBuilding)
            {
                _timers.Remove(key);
            }
        }
    } 
}

