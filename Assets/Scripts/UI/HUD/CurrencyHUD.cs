using Game.Production.Model;
using Game.Production.Tools;
using UniRx;
using UnityEngine;

namespace Game.Production.UI
{
    internal class CurrencyHUD : BaseDisposable
    {
        public struct Ctx
        {
            public IReadOnlyReactiveDictionary<string, EntityWithCount> currency;
            public IResourceLoader resourceLoader;
            public Transform gridForCurrency;
        }

        private readonly Ctx _ctx;
        private ReactiveDictionary<string, ReactiveProperty<EntityWithCount>> _currency;

        public CurrencyHUD(Ctx ctx)
        {
            _currency = new ReactiveDictionary<string, ReactiveProperty<EntityWithCount>>();
            _ctx = ctx;
            foreach (var currencyPair in _ctx.currency)
            {
                AddToCurrency(currencyPair.Key, currencyPair.Value);
            }

            AddDispose(_ctx.currency.ObserveAdd().Subscribe(addEvent => AddToCurrency(addEvent.Key, addEvent.Value)));
            AddDispose(_ctx.currency.ObserveReplace().Subscribe(replaceEvent =>
            {
                if (_currency.TryGetValue(replaceEvent.Key, out ReactiveProperty<EntityWithCount> dynamicCurrency))
                    dynamicCurrency.SetValueAndForceNotify(replaceEvent.NewValue);
                else
                    AddToCurrency(replaceEvent.Key, replaceEvent.NewValue);
            }));
        }

        private void AddToCurrency(string id, EntityWithCount entity)
        {
            ReactiveProperty<EntityWithCount> dynamicCurrency =
                new ReactiveProperty<EntityWithCount>(entity);
            _currency[id] = dynamicCurrency;
            CurrencyField currencyField = new CurrencyField(new CurrencyField.Ctx
            {
                resourceLoader = _ctx.resourceLoader,
                currency = dynamicCurrency,
                gridForCurrency = _ctx.gridForCurrency,
            });
        }
    }
}

