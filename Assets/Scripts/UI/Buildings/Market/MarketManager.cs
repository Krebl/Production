using System;
using System.Collections.Generic;
using Game.Production.Tools;
using Game.Production.Logic;
using Game.Production.Model;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;
using Game.Production.Command;

namespace Game.Production.UI
{
    internal class MarketManager : BaseDisposable
    {
        private const string PREFAB_UI = "Prefabs/MarketView";
        public struct Ctx
        {
            public IResourceLoader resourceLoader;
            public Transform uiContainer;
            public Action close;
            public ICommandExecuter commandExecuter;
            public IReadOnlyLogic logic;
            public IReadOnlyList<CraftItem> availableItems;
        }

        private readonly Ctx _ctx;
        private ReactiveProperty<EntityWithCount> _sellingItem;

        public MarketManager(Ctx ctx)
        {
            _sellingItem = new ReactiveProperty<EntityWithCount>();
            _ctx = ctx;
            LoadOnScene();
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
                close = _ctx.close,
            });
            view.SelectorEntityForSelling.SetCtx(new SelectorEntityView.Ctx
            {
                currentSelect = _sellingItem,
                variants = _ctx.availableItems,
                viewDisposable = viewDisposable,
                resourceLoader = _ctx.resourceLoader,
            });
            view.Cost.SetCtx(new CostView.Ctx
            {
                cost = _sellingItem,
                resourceLoader = _ctx.resourceLoader,
                viewDisposable = viewDisposable
            });
        }

        private void TrySelling()
        {
            if(_sellingItem.Value == null)
                return;
            CraftItem sellingItem = _sellingItem.Value as CraftItem;
            if(sellingItem == null)
                return;
            if(_ctx.logic.Inventory.EnoughCraftItem(sellingItem.Id, sellingItem.Count))
                _ctx.commandExecuter.Execute(new InstructionSellCraftItem(new InstructionSellCraftItem.Ctx
                {
                    sellingItem = sellingItem
                }));
        }
    }
}