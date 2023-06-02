using System;
using System.Collections;
using System.Collections.Generic;
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
        private IDisposable _market;

        public ManagerUIBuilding(Ctx ctx)
        {
            _ctx = ctx;
            AddDispose(_ctx.openMarket.SubscribeWithSkip(OpenMarket));
        }

        private void OpenMarket(string idBuilding)
        {
            if(string.IsNullOrEmpty(idBuilding))
                return;
            _market = new MarketManager(new MarketManager.Ctx
            {
                close = () => _market?.Dispose(),
                availableItems = _ctx.hub.availableItems,
                resourceLoader = _ctx.resourceLoader,
                commandExecuter = _ctx.commandExecuter,
                uiContainer = _ctx.uiContainer,
                logic = _ctx.logic,
            });
        }

        protected override void OnDispose()
        {
            _market?.Dispose();
            base.OnDispose();
        }
    }
}

