using System.Collections.Generic;
using Game.Production.Tools;
using Game.Production.Model;
using UniRx;

namespace Game.Production.Logic
{
    internal class BankLogic : BaseDisposable, IReadOnlyBankLogic
    {
        public struct Ctx
        {
            public IReadOnlyList<EntityWithCount> availableCurrency;
            public ReactiveDictionary<string, EntityWithCount> currency;
        }

        private readonly Ctx _ctx;

        public BankLogic(Ctx ctx)
        {
            _ctx = ctx;
            foreach (var currency in _ctx.availableCurrency)
            {
                _ctx.currency[currency.Id] = currency;
            }
        }

        public bool EnoughCurrency(string id, int count)
        {
            if (string.IsNullOrEmpty(id))
                return false;
            if (_ctx.currency.TryGetValue(id, out EntityWithCount currency))
            {
                return currency.Count >= count;
            }

            return false;
        }

        public IReadOnlyReactiveDictionary<string, EntityWithCount> Currency => _ctx.currency;

        public void IncreaseCurrency(EntityWithCount diff)
        {
            if (_ctx.currency.TryGetValue(diff.Id, out EntityWithCount currency))
            {
                currency.Count += diff.Count;
                _ctx.currency[diff.Id] = currency;
            }
        }
        
        public void DecreaseCurrency(EntityWithCount diff)
        {
            if (_ctx.currency.TryGetValue(diff.Id, out EntityWithCount currency))
            {
                if (diff.Count <= currency.Count)
                {
                    currency.Count -= diff.Count;
                    _ctx.currency[diff.Id] = currency;
                }
            }
        }
    } 
}

