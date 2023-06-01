using System;
using UnityEngine;
using Game.Production.Tools;
using UniRx;
using UnityEngine.UI;

namespace Game.Production.UI
{
    internal class ProductionResourceView : BaseMonoBehaviour
    {
        public struct Ctx
        {
            public CompositeDisposable viewDisposable;
            public Action close;
            public Action start;
            public Action stop;
            public IReadOnlyReactiveProperty<bool> isProcessState;
        }

        [SerializeField] private Button _buttonClose;
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonStop;

        [SerializeField] private SelectorEntityView _selectorResource;

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            _buttonClose.OnClickAsObservable().Subscribe(_ => _ctx.close?.Invoke()).AddTo(_ctx.viewDisposable);
            _buttonStart.OnClickAsObservable().Subscribe(_ => _ctx.start?.Invoke()).AddTo(_ctx.viewDisposable);
            _buttonStop.OnClickAsObservable().Subscribe(_ => _ctx.stop?.Invoke()).AddTo(_ctx.viewDisposable);
            _ctx.isProcessState.Subscribe(isProcess =>
            {
                _buttonStart.gameObject.SetActive(!isProcess);
                _buttonStop.gameObject.SetActive(isProcess);
            }).AddTo(_ctx.viewDisposable);
        }

        public SelectorEntityView SelectorResource => _selectorResource;
    }
}

