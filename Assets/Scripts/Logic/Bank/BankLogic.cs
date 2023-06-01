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
            public List<EntityWithCount> availableCurrency;
        }

        private readonly Ctx _ctx;
        private ReactiveDictionary<string, EntityWithCount> _currency;

        public BankLogic(Ctx ctx)
        {
            _currency = new ReactiveDictionary<string, EntityWithCount>();
            _ctx = ctx;
        }

        public bool EnoughCurrency(string id, int count)
        {
            if (string.IsNullOrEmpty(id))
                return false;
            if (_currency.TryGetValue(id, out EntityWithCount currency))
            {
                return currency.Count >= count;
            }

            return false;
        }

        public void IncreaseCurrency(EntityWithCount diff)
        {
            if (_currency.TryGetValue(diff.Id, out EntityWithCount currency))
            {
                currency.Count += diff.Count;
                _currency[diff.Id] = currency;
            }
        }
        
        public void DecreaseCurrency(EntityWithCount diff)
        {
            if (_currency.TryGetValue(diff.Id, out EntityWithCount currency))
            {
                if (diff.Count <= currency.Count)
                {
                    currency.Count -= diff.Count;
                    _currency[diff.Id] = currency;
                }
            }
        }
    } 
}

