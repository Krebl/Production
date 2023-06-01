using Game.Production.Tools;
using UniRx;

namespace Game.Production.Logic
{
    internal class ProductionLogic : BaseDisposable
    {
        public struct Ctx
        {
            
        }

        private Ctx _ctx;
        private ReactiveDictionary<string, ReactiveProperty<int>> _timers;

        public ProductionLogic(Ctx ctx)
        {
            _timers = new ReactiveDictionary<string, ReactiveProperty<int>>();
            _ctx = ctx;
        }

        public void StartProduction(string idBuilding)
        {
            
        }

        public void StopProduction(string idBuilding)
        {
            
        }
    } 
}

