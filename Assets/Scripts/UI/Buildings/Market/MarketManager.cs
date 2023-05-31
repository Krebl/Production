using Game.Production.Tools;

namespace Game.Production.UI
{
    internal class MarketManager : BaseDisposable
    {
        public struct Ctx
        {
            
        }

        private readonly Ctx _ctx;

        public MarketManager(Ctx ctx)
        {
            _ctx = ctx;
        }
    }
}