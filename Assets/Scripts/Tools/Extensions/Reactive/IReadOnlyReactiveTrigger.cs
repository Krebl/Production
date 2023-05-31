using System;

namespace Game.Production.Tools.Reactive
{
  internal interface IReadOnlyReactiveTrigger
  {
    IDisposable Subscribe(Action action);
    IDisposable SubscribeOnce(Action action);
  }
}