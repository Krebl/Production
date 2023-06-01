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
            public ReactiveProperty<int> countProductionResourceBuilding;
        }

        [SerializeField]
        private Button _buttonStart;
        [SerializeField] 
        private Toggle select1;
        [SerializeField] 
        private Toggle select2;
        [SerializeField] 
        private Toggle select3;
        
        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            _buttonStart.OnClickAsObservable().Subscribe(_ => _ctx.startGame?.Invoke()).AddTo(_ctx.viewDisposable);
            select1.onValueChanged.AddListener(selected => SetCount(selected, 1));
            select2.onValueChanged.AddListener(selected => SetCount(selected, 2));
            select3.onValueChanged.AddListener(selected => SetCount(selected, 3));

            void SetCount(bool selected, int count)
            {
                if (selected)
                    _ctx.countProductionResourceBuilding.Value = count;
            }
        }

        protected override void OnDestroy()
        {
            DisposeToggle(select1);
            DisposeToggle(select2);
            DisposeToggle(select3);
            base.OnDestroy();

            void DisposeToggle(Toggle select)
            {
                if(select != null)
                    select.onValueChanged.RemoveAllListeners(); 
            }
        }
    }
}

