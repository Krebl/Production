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
            public ReactiveProperty<EntityWithCount> currentSelect;
            public IReadOnlyList<EntityWithCount> variants;
            public IResourceLoader resourceLoader;
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
            _buttonNext.OnClickAsObservable().Subscribe(_ => SelectNext()).AddTo(_ctx.viewDisposable);
        }

        private void SelectNext()
        {
            _indexVariant++;
            if (_indexVariant >= _ctx.variants.Count)
            {
                _ctx.currentSelect.Value = null;
                SetDefaultIndex();
                _icon.sprite = null;
                _labelName.text = string.Empty;
                _labelCount.text = string.Empty;
            }
            else
            {
                EntityWithCount variant = _ctx.variants[_indexVariant];
                _ctx.currentSelect.Value = variant;
                _labelName.text = variant.Name;
                _labelCount.text = variant.Count.ToString();
                _icon.sprite = _ctx.resourceLoader.LoadSprite(variant.IconPath);
            }
        }

        private void SetDefaultIndex()
        {
            _indexVariant = EMPTY_VARIANT; 
        }
    }
}

