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
    internal class ProductionResourceManager : BaseDisposable
    {
        private const string PREFAB_UI = "Prefabs/ProductionResourceView";
        
        public struct Ctx
        {
            public IResourceLoader resourceLoader;
            public Transform uiContainer;
            public Action close;
            public ICommandExecuter commandExecuter;
            public IReadOnlyList<EntityWithCount> availableResource;
        }

        private readonly Ctx _ctx;
        private readonly ReactiveProperty<EntityWithCount> _selectedResource;

        public ProductionResourceManager(Ctx ctx)
        {
            _selectedResource = new ReactiveProperty<EntityWithCount>();
            _ctx = ctx;
            LoadOnScene();
        }

        private void LoadOnScene()
        {
            GameObject prefab = _ctx.resourceLoader.LoadPrefab(PREFAB_UI);
            GameObject objOnScene = AddComponent(Object.Instantiate(prefab, _ctx.uiContainer, false));
            ProductionResourceView view = objOnScene.GetComponent<ProductionResourceView>();
            CompositeDisposable viewDisposable = AddDispose(new CompositeDisposable());
            ReactiveProperty<bool> isProcess = new ReactiveProperty<bool>();

            view.SetCtx(new ProductionResourceView.Ctx
            {
                viewDisposable = viewDisposable,
                start = StartProduction,
                stop = null,
                close = _ctx.close,
                isProcessState = isProcess
            });
            view.SelectorResource.SetCtx(new SelectorEntityView.Ctx
            {
                viewDisposable = viewDisposable,
                variants = _ctx.availableResource,
                resourceLoader = _ctx.resourceLoader,
                currentSelect = _selectedResource
            });
        }

        private void StartProduction()
        {
            if(_selectedResource.Value == null)
                return;
            _ctx.commandExecuter.Execute(new InstructionProductionResource(new InstructionProductionResource.Ctx
            {
                
            }));
        }
    }
}

