using System;
using System.Collections.Generic;
using Game.Production.Logic;
using Game.Production.Model;
using Game.Production.Tools;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Production.UI
{
    internal class InventoryManager : BaseDisposable
    {
        private const string PREFAB_UI = "Prefabs/InventoryView";

        public struct Ctx
        {
            public IResourceLoader resourceLoader;
            public Transform uiContainer;
            public Action close;
            public IReadOnlyInventoryLogic logic;
            public IReadOnlyList<CraftItem> availableItems;
            public IReadOnlyList<EntityWithCount> availableResources;
        }

        private readonly Ctx _ctx;

        public InventoryManager(Ctx ctx)
        {
            _ctx = ctx;
            LoadOnScene();
        }
        
        private void LoadOnScene()
        {
            GameObject prefab = _ctx.resourceLoader.LoadPrefab(PREFAB_UI);
            GameObject objOnScene = AddComponent(Object.Instantiate(prefab, _ctx.uiContainer, false));
            InventoryView view = objOnScene.GetComponent<InventoryView>();
            CompositeDisposable viewDisposable = AddDispose(new CompositeDisposable());
            
            view.SetCtx(new InventoryView.Ctx
            {
                viewDisposable = viewDisposable,
                close = _ctx.close,
            });

            GridInventory resourceInventory = new GridInventory(new GridInventory.Ctx
            {
                resourceLoader = _ctx.resourceLoader,
                elements = _ctx.availableResources,
                grid = view.GridForResources,
                logic = _ctx.logic,
            });
            AddDispose(resourceInventory);
            GridInventory craftItemInventory = new GridInventory(new GridInventory.Ctx
            {
                resourceLoader = _ctx.resourceLoader,
                elements = _ctx.availableItems,
                grid = view.GridForCraftItem,
                logic = _ctx.logic,
            });
            AddDispose(craftItemInventory);
        }
    }
}

