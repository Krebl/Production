using Game.Production.Tools;

namespace Game.Production.UI
{
    internal class CraftItemManager : BaseDisposable
    {
        public struct Ctx
        {
            
        }

        private readonly Ctx _ctx;

        public CraftItemManager(Ctx ctx)
        {
            _ctx = ctx;
        }
    }
}

