using System;
using UniRx;

namespace Game.Production.Tools
{
    internal class TimersTools : BaseDisposable
    {
        public struct Ctx
        {
            public IReadOnlyReactiveDictionary<string, ReactiveProperty<int>> timers;
        }

        private readonly Ctx _ctx;

        public TimersTools(Ctx ctx)
        {
            _ctx = ctx;
            AddDispose(Observable.Interval(TimeSpan.FromSeconds(1), Scheduler.MainThreadIgnoreTimeScale).Subscribe(_ =>
            {
                foreach (var timerPair in _ctx.timers)
                {
                    if(timerPair.Value.Value <= 0)
                        continue;
                    timerPair.Value.Value--;
                }
            }));
        }
    }
}

