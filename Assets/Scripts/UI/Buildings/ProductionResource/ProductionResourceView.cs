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
        }

        [SerializeField] private Button _buttonClose;

        private CraftItemView.Ctx _ctx;

        public void SetCtx(CraftItemView.Ctx ctx)
        {
            _ctx = ctx;
            _buttonClose.OnClickAsObservable().Subscribe(_ => _ctx.close?.Invoke()).AddTo(_ctx.viewDisposable);
        }
    }
}

