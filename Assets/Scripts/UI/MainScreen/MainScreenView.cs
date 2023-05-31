using System;
using UnityEngine;
using Game.Production.Tools;
using UniRx;
using UnityEngine.UI;

namespace Game.Production.UI
{
    internal class MainScreenView : BaseMonoBehaviour
    {
        public struct Ctx
        {
            public CompositeDisposable viewDisposable;
            public Action startGame;
        }

        [SerializeField]
        private Button _buttonStart;

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            _buttonStart.OnClickAsObservable().Subscribe(_ => _ctx.startGame?.Invoke()).AddTo(_ctx.viewDisposable);
        }
    }
}

