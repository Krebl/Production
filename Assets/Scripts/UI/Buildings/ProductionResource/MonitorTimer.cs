using System;
using System.Collections.Generic;
using Game.Production.Tools;
using UniRx;

namespace Game.Production.UI
{
    internal class MonitorTimer : BaseDisposable
    {
        public struct Ctx
        {
            public IReadOnlyReactiveDictionary<string, ReactiveProperty<int>> timers;
            public Action<string> onCompleted;
        }

        private readonly Ctx _ctx;
        private Dictionary<string, List<IDisposable>> _timersDisposable;

        public MonitorTimer(Ctx ctx)
        {
            _ctx = ctx;
            _timersDisposable = new Dictionary<string, List<IDisposable>>();
            AddDispose(_ctx.timers.ObserveAdd().Subscribe(addEvent =>
            {
                if (_timersDisposable.TryGetValue(addEvent.Key, out List<IDisposable> subscriptions))
                {
                    foreach (var subscription in subscriptions)
                    {
                        subscription?.Dispose();
                    }
                    subscriptions.Clear();
                }

                if (subscriptions == null)
                    subscriptions = new List<IDisposable>();
                subscriptions.Add(addEvent.Value.Subscribe(secondsLeft =>
                {
                    if(secondsLeft <= 0)
                        _ctx.onCompleted?.Invoke(addEvent.Key);
                }));
            }));
            AddDispose(_ctx.timers.ObserveRemove().Subscribe(removeEvent =>
            {
                if (_timersDisposable.TryGetValue(removeEvent.Key, out List<IDisposable> subscriptions))
                {
                    foreach (var subscription in subscriptions)
                    {
                        subscription?.Dispose();
                    }
                    subscriptions.Clear();
                }

                _timersDisposable.Remove(removeEvent.Key);
            }));
        }
    }    
}

