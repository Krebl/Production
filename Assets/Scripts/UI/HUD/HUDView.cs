using System;
using Game.Production.Tools;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Production.UI
{
    internal class HUDView : BaseMonoBehaviour
    {
        public struct Ctx
        {
            public Action openInventory;
            public CompositeDisposable viewDisposable;
        }

        [SerializeField] private Button _buttonInventory;
        [SerializeField] private Transform _currencyGrid;

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            _buttonInventory.OnClickAsObservable().Subscribe(_ => _ctx.openInventory?.Invoke()).AddTo(_ctx.viewDisposable);
        }

        public Transform CurrencyGrid => _currencyGrid;
    }
}

