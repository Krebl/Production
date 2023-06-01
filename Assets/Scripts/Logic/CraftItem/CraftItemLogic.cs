using System.Collections.Generic;
using Game.Production.Model;
using Game.Production.Tools;
using UniRx;

namespace Game.Production.Logic
{
    internal class CraftItemLogic : BaseDisposable, IReadOnlyCraftItemLogic
    {
        public struct Ctx
        {
            public IReadOnlyList<Receipt> receipts;
        }

        private readonly Ctx _ctx;
        private ReactiveDictionary<string, ReactiveProperty<int>> _timers;

        public CraftItemLogic(Ctx ctx)
        {
            _timers = new ReactiveDictionary<string, ReactiveProperty<int>>();
            TimersTools timersTools = new TimersTools(new TimersTools.Ctx
            {
                timers = _timers
            });
            AddDispose(timersTools);
            _ctx = ctx;
        }

        public bool CanCraft(EntityWithCount[] ingredients)
        {
            bool found = false;
            foreach (var receipt in _ctx.receipts)
            {
                for (int i = 0; i < ingredients.Length; i++)
                {
                    if (!receipt.CostCraft.Exists(cost => cost.Id == ingredients[i].Id))
                    {
                        found = false;
                        break;
                    }
                    else
                        found = true;
                }

                if (found)
                    return true;
            }

            return false;
        }

        public IReadOnlyReactiveDictionary<string, ReactiveProperty<int>> Timers => _timers;
    }
}

