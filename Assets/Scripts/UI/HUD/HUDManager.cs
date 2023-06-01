using System;
using Game.Production.Model;
using UnityEngine;
using Game.Production.Tools;
using UniRx;
using Object = UnityEngine.Object;

namespace Game.Production.UI
{
    internal class HUDManager : BaseDisposable
    {
        private const string PREFAB_UI = "Prefabs/HUDPanel";
        
        public struct Ctx
        {
            public Action openInventory;
            public IResourceLoader resourceLoader;
            public Transform uiContainer;
            public IReadOnlyReactiveDictionary<string, EntityWithCount> currency;
        }

        private readonly Ctx _ctx;

        public HUDManager(Ctx ctx)
        {
            _ctx = ctx;
            LoadOnScene();
        }

        private void LoadOnScene()
        {
            GameObject prefab = _ctx.resourceLoader.LoadPrefab(PREFAB_UI);
            GameObject objOnScene = AddComponent(Object.Instantiate(prefab, _ctx.uiContainer, false));
            HUDView view = objOnScene.GetComponent<HUDView>();
            CompositeDisposable viewDisposable = AddDispose(new CompositeDisposable());
            view.SetCtx(new HUDView.Ctx
            {
                viewDisposable = viewDisposable,
                openInventory = _ctx.openInventory,
            });
            CurrencyHUD currencyHUD = new CurrencyHUD(new CurrencyHUD.Ctx
            {
                resourceLoader = _ctx.resourceLoader,
                currency = _ctx.currency,
                gridForCurrency = view.CurrencyGrid
            });
            AddDispose(currencyHUD);
        }
    }
}

