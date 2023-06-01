using System;
using System.Collections.Generic;
using Game.Production.Command;
using Game.Production.Model;
using Game.Production.Tools;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Production.UI
{
    internal class CraftItemManager : BaseDisposable
    {
        private const string PREFAB_UI = "Prefabs/ProductionResourceView";
        
        public struct Ctx
        {
            public IResourceLoader resourceLoader;
            public Transform uiContainer;
            public Action close;
            public ICommandExecuter commandExecuter;
            public IReadOnlyList<CraftItem> availableitem;
            public IReadOnlyList<EntityWithCount> availableResource;
        }

        private readonly Ctx _ctx;
        private readonly ReactiveProperty<EntityWithCount> _firstIngredient;
        private readonly ReactiveProperty<EntityWithCount> _secondIngredient;
        private readonly ReactiveProperty<CraftItem> _result;

        public CraftItemManager(Ctx ctx)
        {
            _firstIngredient = new ReactiveProperty<EntityWithCount>();
            _secondIngredient = new ReactiveProperty<EntityWithCount>();
            _result = new ReactiveProperty<CraftItem>();
            _ctx = ctx;
            LoadOnScene();
        }
        
        private void LoadOnScene()
        {
            GameObject prefab = _ctx.resourceLoader.LoadPrefab(PREFAB_UI);
            GameObject objOnScene = AddComponent(Object.Instantiate(prefab, _ctx.uiContainer, false));
            CraftItemView view = objOnScene.GetComponent<CraftItemView>();
            CompositeDisposable viewDisposable = AddDispose(new CompositeDisposable());
            ReactiveProperty<bool> isProcess = new ReactiveProperty<bool>();

            view.SetCtx(new CraftItemView.Ctx
            {
                viewDisposable = viewDisposable,
                start = StartCraft,
                stop = StopCraft,
                close = _ctx.close,
                isProcessState = isProcess,
                resourceLoader = _ctx.resourceLoader,
                resultItem = _result
            });
            view.FirstIngredient.SetCtx(new SelectorEntityView.Ctx
            {
                resourceLoader = _ctx.resourceLoader,
                variants = _ctx.availableResource,
                viewDisposable = viewDisposable,
                currentSelect = _firstIngredient
            });
            view.SecondIngredient.SetCtx(new SelectorEntityView.Ctx
            {
                resourceLoader = _ctx.resourceLoader,
                variants = _ctx.availableResource,
                viewDisposable = viewDisposable,
                currentSelect = _secondIngredient
            });
        }

        private void StartCraft()
        {
            
        }

        private void StopCraft()
        {
            
        }
    }
}

