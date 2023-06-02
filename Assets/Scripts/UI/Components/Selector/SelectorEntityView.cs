using System.Collections.Generic;
using UnityEngine;
using Game.Production.Tools;
using UniRx;
using Game.Production.Model;
using TMPro;
using UnityEngine.UI;

namespace Game.Production.UI
{
    internal class SelectorEntityView : BaseMonoBehaviour
    {
        public struct Ctx
        {
            public CompositeDisposable viewDisposable;
            public IReactiveProperty<EntityWithCount> currentSelect;
            public IReadOnlyList<EntityWithCount> variants;
            public IResourceLoader resourceLoader;
            public IReadOnlyReactiveProperty<bool> interactable;
        }

        [SerializeField] 
        private Button _buttonNext;
        [SerializeField] 
        private Image _icon;
        [SerializeField] 
        private TextMeshProUGUI _labelCount;
        [SerializeField] 
        private TextMeshProUGUI _labelName;


        private Ctx _ctx;
        private const int EMPTY_VARIANT = -1;
        private int _indexVariant;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            SetDefaultIndex();
            _ctx.interactable.Subscribe(interactable => _buttonNext.interactable = interactable)
                .AddTo(_ctx.viewDisposable);
            _buttonNext.OnClickAsObservable().Subscribe(_ => SelectNext()).AddTo(_ctx.viewDisposable);
            _ctx.currentSelect.Subscribe(selected =>
            {
                if(selected == null)
                {
                    _icon.sprite = null;
                    _labelName.text = string.Empty;
                    _labelCount.text = string.Empty;
                    return;
                }
                _labelName.text = selected.Name;
                _labelCount.text = selected.Count.ToString();
                _icon.sprite = _ctx.resourceLoader.LoadSprite(selected.IconPath);
                
            }).AddTo(_ctx.viewDisposable);
        }

        private void SelectNext()
        {
            _indexVariant++;
            if (_indexVariant >= _ctx.variants.Count)
            {
                _ctx.currentSelect.Value = null;
                SetDefaultIndex();
            }
            else
            {
                EntityWithCount variant = _ctx.variants[_indexVariant];
                _ctx.currentSelect.Value = variant;
            }
        }

        private void SetDefaultIndex()
        {
            _indexVariant = EMPTY_VARIANT; 
        }
    }
}

