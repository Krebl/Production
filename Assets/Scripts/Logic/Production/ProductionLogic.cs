using Game.Production.Tools;

namespace Game.Production.Logic
{
    internal class ProductionLogic : BaseDisposable
    {
        public struct Ctx
        {
            
        }

        private Ctx _ctx;

        public ProductionLogic(Ctx ctx)
        {
            _ctx = ctx;
        }
    } 
}

