using Game.Production.Tools;

namespace Game.Production.UI
{
    internal class ProductionResourceManager : BaseDisposable
    {
        public struct Ctx
        {
            
        }

        private readonly Ctx _ctx;

        public ProductionResourceManager(Ctx ctx)
        {
            _ctx = ctx;
        }
    }
}

