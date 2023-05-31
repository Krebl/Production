using Game.Production.Tools;

namespace Game.Production.UI
{
    internal class InventoryManager : BaseDisposable
    {
        public struct Ctx
        {
            
        }

        private readonly Ctx _ctx;

        public InventoryManager(Ctx ctx)
        {
            _ctx = ctx;
        }
    }
}

