using Game.Production.Tools;

namespace Game.Production.UI
{
    internal class MainScreenManager : BaseDisposable
    {
        public struct Ctx
        {
            
        }

        private readonly Ctx _ctx;

        public MainScreenManager(Ctx ctx)
        {
            _ctx = ctx;
        }
    }
}