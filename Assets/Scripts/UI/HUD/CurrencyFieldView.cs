using UnityEngine;
using Game.Production.Tools;
using Game.Production.Model;
using TMPro;
using UniRx;
using UnityEngine.UI;

namespace Game.Production.UI
{
    internal class CurrencyFieldView : BaseMonoBehaviour
    {
        public struct Ctx
        {
            public IReadOnlyReactiveProperty<EntityWithCount> currency;
            public IResourceLoader resourceLoader;
            public CompositeDisposable viewDisposable;
        }

        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI labelCount;

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            icon.sprite = _ctx.resourceLoader.LoadSprite(_ctx.currency.Value.IconPath);
            _ctx.currency.Subscribe(currency => labelCount.text = currency.Count.ToString()).AddTo(_ctx.viewDisposable);
        }
    }
}

