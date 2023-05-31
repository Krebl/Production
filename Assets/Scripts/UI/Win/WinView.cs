using System;
using Game.Production.Tools;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Game.Production.UI
{
    internal class WinView : BaseMonoBehaviour
    {
        public struct Ctx
        {
            public CompositeDisposable viewDisposable;
            public Action returnToMain;
        }

        [SerializeField] 
        private Button buttonReturnToMain;

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            buttonReturnToMain.OnClickAsObservable().Subscribe(_ => _ctx.returnToMain?.Invoke())
                .AddTo(_ctx.viewDisposable);
        }
    }
}

