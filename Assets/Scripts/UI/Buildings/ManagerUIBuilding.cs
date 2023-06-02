using System;
using Game.Production.Command;
using Game.Production.Logic;
using Game.Production.Model;
using UnityEngine;
using Game.Production.Tools;
using Game.Production.Tools.Reactive;

namespace Game.Production.UI
{
    internal class ManagerUIBuilding : BaseDisposable
    {
        public struct Ctx
        {
            public IReadOnlyReactiveEvent<string> openMarket;
            public IReadOnlyReactiveEvent<string> openCraft;
            public IReadOnlyReactiveEvent<string> openProduction;
            public IResourceLoader resourceLoader;
            public Transform uiContainer;
            public ICommandExecuter commandExecuter;
            public IReadOnlyLogic logic;
            public Hub hub;
        }

        private Ctx _ctx;
        private IDisposable _window;

        public ManagerUIBuilding(Ctx ctx)
        {
            _ctx = ctx;
            AddDispose(_ctx.openMarket.SubscribeWithSkip(OpenMarket));
            AddDispose(_ctx.openProduction.SubscribeWithSkip(OpenProduction));
            AddDispose(_ctx.openCraft.SubscribeWithSkip(OpenCraft));
        }

        private void OpenMarket(string idBuilding)
        {
            if(string.IsNullOrEmpty(idBuilding))
                return;
            _window?.Dispose();
            _window = new MarketManager(new MarketManager.Ctx
            {
                close = () => _window?.Dispose(),
                availableItems = _ctx.hub.availableItems,
                resourceLoader = _ctx.resourceLoader,
                commandExecuter = _ctx.commandExecuter,
                uiContainer = _ctx.uiContainer,
                logic = _ctx.logic,
            });
        }
        
        private void OpenProduction(string idBuilding)
        {
            if(string.IsNullOrEmpty(idBuilding))
                return;
            _window?.Dispose();
            _window = new ProductionResourceManager(new ProductionResourceManager.Ctx
            {
                commandExecuter = _ctx.commandExecuter,
                close = () => _window?.Dispose(),
                availableResource = _ctx.hub.availableResource,
                resourceLoader = _ctx.resourceLoader,
                uiContainer = _ctx.uiContainer,
                logic = _ctx.logic.Production,
                idBuiilding = idBuilding
            });
        }
        
        private void OpenCraft(string idBuilding)
        {
            if(string.IsNullOrEmpty(idBuilding))
                return;
            _window?.Dispose();
            _window = new CraftItemManager(new CraftItemManager.Ctx
            {
                close = () => _window?.Dispose(),
                availableResource = _ctx.hub.availableResource,
                availableitem = _ctx.hub.availableItems,
                resourceLoader = _ctx.resourceLoader,
                commandExecuter = _ctx.commandExecuter,
                idBuilding = idBuilding,
                logic = _ctx.logic.CraftItem,
                uiContainer = _ctx.uiContainer
            });
        }
        

        protected override void OnDispose()
        {
            _window?.Dispose();
            base.OnDispose();
        }
    }
}

