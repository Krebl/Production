using System;
using UnityEngine;
using Game.Production.Tools;
using UniRx;
using UnityEngine.UI;

namespace Game.Production.UI
{
    internal class MarketView : BaseMonoBehaviour
    {
        public struct Ctx
        {
            public CompositeDisposable viewDisposable;
            public Action close;
            public Action selling;
        }

        [SerializeField] private Button _buttonClose;
        [SerializeField] private Button _buttonSelling;
        [SerializeField] private SelectorEntityView _selectorEntityForSelling;
        [SerializeField] private CostView _cost;

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            _buttonClose.OnClickAsObservable().Subscribe(_ => _ctx.close?.Invoke()).AddTo(_ctx.viewDisposable);
            _buttonSelling.OnClickAsObservable().Subscribe(_ => _ctx.selling?.Invoke()).AddTo(_ctx.viewDisposable);
        }

        public SelectorEntityView SelectorEntityForSelling => _selectorEntityForSelling;

        public CostView Cost => _cost;
    } 
}

