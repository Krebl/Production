using Game.Production.Tools;
using Game.Production.Model;
using UniRx;

namespace Game.Production.Logic
{
    internal class WinLogic : BaseDisposable, IWinLogic
    {
        public struct Ctx
        {
            public IReadOnlyReactiveDictionary<string, EntityWithCount> currency;
            public EntityWithCount winCondition;
        }

        private readonly Ctx _ctx;
        private readonly ReactiveProperty<bool> _isWin;

        public WinLogic(Ctx ctx)
        {
            _isWin = new ReactiveProperty<bool>();
            _ctx = ctx;
            AddDispose(_ctx.currency.ObserveReplace().Subscribe(replaceEvent =>
            {
                if (replaceEvent.Key != _ctx.winCondition.Id)
                    return;
                _isWin.Value = replaceEvent.NewValue.Count >= _ctx.winCondition.Count;
            }));
        }

        public IReadOnlyReactiveProperty<bool> IsWin => _isWin;
    }
}

