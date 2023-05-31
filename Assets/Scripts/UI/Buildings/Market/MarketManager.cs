using System;
using Game.Production.Tools;
using Game.Production.Model;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Production.UI
{
    internal class MarketManager : BaseDisposable
    {
        private const string PREFAB_UI = "";
        public struct Ctx
        {
            public Building building;
            public IResourceLoader resourceLoader;
            public Transform uiContainer;
            public Action close;
        }

        private readonly Ctx _ctx;

        public MarketManager(Ctx ctx)
        {
            _ctx = ctx;
        }

        private void LoadOnScene()
        {
            GameObject prefab = _ctx.resourceLoader.LoadPrefab(PREFAB_UI);
            GameObject objOnScene = AddComponent(Object.Instantiate(prefab, _ctx.uiContainer, false));
            MarketView view = objOnScene.GetComponent<MarketView>();

            CompositeDisposable viewDisposable = AddDispose(new CompositeDisposable());
            view.SetCtx(new MarketView.Ctx
            {
                viewDisposable = viewDisposable,
                selling = TrySelling,
                close = _ctx.close
            });
            view.SelectorEntityForSelling.SetCtx(new SelectorEntityView.Ctx
            {
    
            });
            view.Cost.SetCtx(new CostView.Ctx
            {
                
            });
        }

        private void TrySelling()
        {
            
        }
    }
}