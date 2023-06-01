using UnityEngine;
using Game.Production.Tools;
using Game.Production.Model;
using UniRx;

namespace Game.Production.UI
{
    internal class CurrencyField : BaseDisposable
    {
        private const string PREFAB_UI = "Prefabs/CurrencyField";
        public struct Ctx
        {
            public IResourceLoader resourceLoader;
            public IReadOnlyReactiveProperty<EntityWithCount> currency;
            public Transform gridForCurrency;
        }

        private readonly Ctx _ctx;

        public CurrencyField(Ctx ctx)
        {
            _ctx = ctx;
            LoadOnScene();
        }

        private void LoadOnScene()
        {
            GameObject prefab = _ctx.resourceLoader.LoadPrefab(PREFAB_UI);
            GameObject objOnScene = AddComponent(Object.Instantiate(prefab, _ctx.gridForCurrency, false));
            CurrencyFieldView view = objOnScene.GetComponent<CurrencyFieldView>();
            CompositeDisposable viewDisposable = AddDispose(new CompositeDisposable());
            view.SetCtx(new CurrencyFieldView.Ctx
            {
                currency = _ctx.currency,
                resourceLoader = _ctx.resourceLoader,
                viewDisposable = viewDisposable
            });
        }
    }
}

