using System;
using UnityEngine;
using Game.Production.Tools;
using UniRx;
using UnityEngine.UI;

namespace Game.Production.UI
{
    internal class InventoryView : BaseMonoBehaviour
    {
        public struct Ctx
        {
            public CompositeDisposable viewDisposable;
            public Action close;
        }

        [SerializeField] private Button _buttonClose;
        [SerializeField] private Transform _gridForCraftItem;
        [SerializeField] private Transform _gridForResources;

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            _buttonClose.OnClickAsObservable().Subscribe(_ => _ctx.close?.Invoke()).AddTo(_ctx.viewDisposable);
        }

        public Transform GridForCraftItem => _gridForCraftItem;
        public Transform GridForResources => _gridForResources;
    } 
}

