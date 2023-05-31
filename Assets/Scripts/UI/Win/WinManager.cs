using Game.Production.Tools;

namespace Game.Production.UI
{
    internal class WinManager : BaseDisposable
    {
        public struct Ctx
        {
            
        }

        private readonly Ctx _ctx;

        public WinManager(Ctx ctx)
        {
            _ctx = ctx;
        }
    }
}

