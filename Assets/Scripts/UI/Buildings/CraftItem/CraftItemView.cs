using System;
using UnityEngine;
using Game.Production.Tools;
using UniRx;
using UnityEngine.UI;
using Game.Production.Model;
using TMPro;

namespace Game.Production.UI
{
    internal class CraftItemView : BaseMonoBehaviour
    {
        public struct Ctx
        {
            public CompositeDisposable viewDisposable;
            public Action close;
            public Action start;
            public Action stop;
            public IReadOnlyReactiveProperty<bool> isProcessState;
            public IReadOnlyReactiveProperty<CraftItem> resultItem;
            public IResourceLoader resourceLoader;
            public IReadOnlyReactiveProperty<int> secondsLeftForEndCraft;
        }

        [SerializeField] private Button _buttonClose;
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonStop;

        private Ctx _ctx;
        
        [SerializeField] private SelectorEntityView _firstIngredient;
        [SerializeField] private SelectorEntityView _secondIngredient;
        [SerializeField] private Image iconResult;
        [SerializeField] private TextMeshProUGUI _labelTimer;

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
            _ctx.resultItem.Subscribe(result =>
            {
                if (result == null)
                {
                    iconResult.sprite = null;
                    return;
                }
                iconResult.sprite = _ctx.resourceLoader.LoadSprite(result.IconPath);
            }).AddTo(_ctx.viewDisposable);
            _ctx.secondsLeftForEndCraft.Subscribe(seconds =>
            {
                _labelTimer.text = seconds.ToString();
            }).AddTo(_ctx.viewDisposable);
        }

        public SelectorEntityView FirstIngredient => _firstIngredient;
        public SelectorEntityView SecondIngredient => _secondIngredient;
    } 
}