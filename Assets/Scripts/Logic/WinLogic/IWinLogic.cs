using UniRx;

namespace Game.Production.Logic
{
    internal interface IWinLogic
    {
        public IReadOnlyReactiveProperty<bool> IsWin { get; }
    }
}

