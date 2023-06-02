using System;
using System.Collections.Generic;
using Game.Production.Command;
using Game.Production.Model;
using Game.Production.Logic;
using Game.Production.Tools;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Production.UI
{
    internal class CraftItemManager : BaseDisposable
    {
        private const string PREFAB_UI = "Prefabs/CraftItemView";
        
        public struct Ctx
        {
            public IResourceLoader resourceLoader;
            public Transform uiContainer;
            public Action close;
            public ICommandExecuter commandExecuter;
            public IReadOnlyList<CraftItem> availableitem;
            public IReadOnlyList<EntityWithCount> availableResource;
            public IReadOnlyCraftItemLogic logic;
            public string idBuilding;
        }

        private readonly Ctx _ctx;
        private readonly ReactiveProperty<EntityWithCount> _firstIngredient;
        private readonly ReactiveProperty<EntityWithCount> _secondIngredient;
        private readonly ReactiveProperty<CraftItem> _result;
        
        private ReactiveProperty<int> _secondsForEndCraft;
        private IDisposable _subscriptionOnTimer;

        public CraftItemManager(Ctx ctx)
        {
            _firstIngredient = new ReactiveProperty<EntityWithCount>();
            _secondIngredient = new ReactiveProperty<EntityWithCount>();
            _result = new ReactiveProperty<CraftItem>();
            _secondsForEndCraft = new ReactiveProperty<int>();
            _ctx = ctx;
            if (_ctx.logic.CurrentCraftingItem.TryGetValue(_ctx.idBuilding, out CraftItem craftItem))
                _result.Value = craftItem;
            if (_ctx.logic.Timers.TryGetValue(_ctx.idBuilding, out ReactiveProperty<int> seconds))
                _subscriptionOnTimer = seconds.Subscribe(secondsLeft => _secondsForEndCraft.Value = secondsLeft);  
            AddDispose(_ctx.logic.Timers.ObserveAdd().Subscribe(addEvent =>
            {
                if (addEvent.Key == _ctx.idBuilding)
                {
                    _subscriptionOnTimer?.Dispose();
                    _subscriptionOnTimer =
                        addEvent.Value.Subscribe(secondsLeft => _secondsForEndCraft.Value = secondsLeft);
                }
            }));
            AddDispose(_ctx.logic.Timers.ObserveRemove().Subscribe(removeEvent =>
            {
                if (removeEvent.Key == _ctx.idBuilding)
                {
                    _subscriptionOnTimer?.Dispose();
                    _secondsForEndCraft.Value = 0;
                }
            }));
            LoadOnScene();
        }
        
        private void LoadOnScene()
        {
            GameObject prefab = _ctx.resourceLoader.LoadPrefab(PREFAB_UI);
            GameObject objOnScene = AddComponent(Object.Instantiate(prefab, _ctx.uiContainer, false));
            CraftItemView view = objOnScene.GetComponent<CraftItemView>();
            CompositeDisposable viewDisposable = AddDispose(new CompositeDisposable());
            ReactiveProperty<bool> isProcess = new ReactiveProperty<bool>();
            ReactiveProperty<bool> interactableSelectors = new ReactiveProperty<bool>();
            AddDispose(isProcess.Subscribe(processing => interactableSelectors.Value = !processing));
            view.SetCtx(new CraftItemView.Ctx
            {
                viewDisposable = viewDisposable,
                start = StartCraft,
                stop = () => StopCraft(true),
                close = _ctx.close,
                isProcessState = isProcess,
                resourceLoader = _ctx.resourceLoader,
                resultItem = _result,
                secondsLeftForEndCraft = _secondsForEndCraft
            });
            view.FirstIngredient.SetCtx(new SelectorEntityView.Ctx
            {
                resourceLoader = _ctx.resourceLoader,
                variants = _ctx.availableResource,
                viewDisposable = viewDisposable,
                currentSelect = _firstIngredient,
                interactable = interactableSelectors
            });
            view.SecondIngredient.SetCtx(new SelectorEntityView.Ctx
            {
                resourceLoader = _ctx.resourceLoader,
                variants = _ctx.availableResource,
                viewDisposable = viewDisposable,
                currentSelect = _secondIngredient,
                interactable = interactableSelectors
            });
            AddDispose(_secondsForEndCraft.Subscribe(seconds =>
            {
                isProcess.Value = seconds > 0;
            }));
            AddDispose(_firstIngredient.Subscribe(_ => DefineReceipt()));
            AddDispose(_secondIngredient.Subscribe(_ => DefineReceipt()));

            void DefineReceipt()
            {
                if (_firstIngredient.Value == null || _secondIngredient.Value == null)
                {
                    _result.Value = null;
                    return;
                }
                Receipt receipt = _ctx.logic.GetReceipt(new EntityWithCount[]
                    {_firstIngredient.Value, _secondIngredient.Value});
                _result.Value = receipt?.Result;
            }
        }

        private void StartCraft()
        {
            if(_result.Value == null)
                return;
            _ctx.commandExecuter.Execute(new InstructionCraftItem(new InstructionCraftItem.Ctx
            {
                craftItem = _result.Value,
                idBuilding = _ctx.idBuilding
            }));
        }

        private void StopCraft(bool isForce)
        {
            if(_result.Value == null)
                return;
            _ctx.commandExecuter.Execute(new InstructionStopCraftItem(new InstructionStopCraftItem.Ctx
            {
                idBuilding = _ctx.idBuilding,
                isForceStop = isForce,
                craftItem = new CraftItem()
                {
                    Id = _result.Value.Id,
                    Name = _result.Value.Name,
                    Count = 1,
                    IconPath = _result.Value.IconPath,
                    SellingCost = _result.Value.SellingCost
                },
            }));
        }

        protected override void OnDispose()
        {
            _subscriptionOnTimer?.Dispose();
            base.OnDispose();
        }
    }
}

