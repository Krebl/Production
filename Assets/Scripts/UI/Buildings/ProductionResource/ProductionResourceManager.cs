using System;
using System.Collections.Generic;
using Game.Production.Command;
using Game.Production.Model;
using Game.Production.Tools;
using Game.Production.Logic;
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
            public string idBuiilding;
            public IReadOnlyProductionLogic logic;
        }

        private readonly Ctx _ctx;
        private readonly ReactiveProperty<EntityWithCount> _selectedResource;
        private ReactiveProperty<bool> _isProcess;
        private ReactiveProperty<int> _secondsForEndProduction;
        private IDisposable _subscriptionOnTimer;

        public ProductionResourceManager(Ctx ctx)
        {
            _isProcess = new ReactiveProperty<bool>();
            _selectedResource = new ReactiveProperty<EntityWithCount>();
            _secondsForEndProduction = new ReactiveProperty<int>();
            if (_ctx.logic.CurrentProductionResource.TryGetValue(_ctx.idBuiilding, out EntityWithCount resource))
                _selectedResource.Value = resource;
            if (_ctx.logic.Timers.TryGetValue(_ctx.idBuiilding, out ReactiveProperty<int> seconds))
                _secondsForEndProduction.Value = seconds.Value;
            AddDispose(_ctx.logic.Timers.ObserveAdd().Subscribe(addEvent =>
            {
                if (addEvent.Key == _ctx.idBuiilding)
                {
                    _subscriptionOnTimer?.Dispose();
                    _subscriptionOnTimer =
                        addEvent.Value.Subscribe(secondsLeft => _secondsForEndProduction.Value = secondsLeft);
                }
            }));
            AddDispose(_ctx.logic.Timers.ObserveRemove().Subscribe(removeEvent =>
            {
                if (removeEvent.Key == _ctx.idBuiilding)
                {
                    _subscriptionOnTimer?.Dispose();
                    _secondsForEndProduction.Value = 0;
                }
            }));
            _ctx = ctx;
            LoadOnScene();
        }

        private void LoadOnScene()
        {
            GameObject prefab = _ctx.resourceLoader.LoadPrefab(PREFAB_UI);
            GameObject objOnScene = AddComponent(Object.Instantiate(prefab, _ctx.uiContainer, false));
            ProductionResourceView view = objOnScene.GetComponent<ProductionResourceView>();
            CompositeDisposable viewDisposable = AddDispose(new CompositeDisposable());
            ReactiveProperty<bool> interactableSelector = new ReactiveProperty<bool>();
            AddDispose(_isProcess.Subscribe(isProcess => interactableSelector.Value = !isProcess));

            view.SetCtx(new ProductionResourceView.Ctx
            {
                viewDisposable = viewDisposable,
                start = StartProduction,
                stop = StopProduction,
                close = _ctx.close,
                isProcessState = _isProcess,
                secondsLeftForEndProduction = _secondsForEndProduction
            });
            view.SelectorResource.SetCtx(new SelectorEntityView.Ctx
            {
                viewDisposable = viewDisposable,
                variants = _ctx.availableResource,
                resourceLoader = _ctx.resourceLoader,
                currentSelect = _selectedResource,
                interactable = interactableSelector
            });
            AddDispose(_secondsForEndProduction.Subscribe(seconds => _isProcess.Value = seconds > 0));
        }

        private void StartProduction()
        {
            if(_selectedResource.Value == null)
                return;
            _ctx.commandExecuter.Execute(new InstructionProductionResource(new InstructionProductionResource.Ctx
            {
                idBuilding = _ctx.idBuiilding,
                idResource = _selectedResource.Value.Id
            }));
        }

        private void StopProduction()
        {
            if(_selectedResource.Value == null)
                return;
            _ctx.commandExecuter.Execute(new InstructionStopProductionResource(new InstructionStopProductionResource.Ctx
            {
                idBuilding = _ctx.idBuiilding,
                isForceStop = true,
                resource = _selectedResource.Value
            }));
        }

        protected override void OnDispose()
        {
            _subscriptionOnTimer?.Dispose();
            base.OnDispose();
        }
    }
}

