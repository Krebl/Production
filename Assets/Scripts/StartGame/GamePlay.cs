using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Production.Tools;
using Game.Production.Command;
using Game.Production.Model;
using Game.Production.Logic;
using Game.Production.UI;
using UniRx;

namespace Game.Production.Start
{
    internal class GamePlay : BaseDisposable
    {
        public struct Ctx
        {
            public IResourceLoader resourceLoader;
            public ICommandExecuter commandExecuter;
            public Hub hub;
            public IReadOnlyLogic logic;
            public Transform uiContainer;
        }

        private readonly Ctx _ctx;
        private IDisposable _mainMenu;
        private IDisposable _winView;
        private IDisposable _inventoryView;

        private IDisposable _location;

        public GamePlay(Ctx ctx)
        {
            _ctx = ctx;
            HUDManager hudManager = new HUDManager(new HUDManager.Ctx
            {
                resourceLoader = _ctx.resourceLoader,
                openInventory = OpenInventory,
                uiContainer = _ctx.uiContainer
            });
            AddDispose(hudManager);
            CreateMainScreen();
            _ctx.logic.Win.IsWin.Subscribe(isWin =>
            {
                if (isWin)
                    CreateWin();
            });
        }

        private void CreateMainScreen()
        {
            ReactiveProperty<int> countProductionResourceBuilding = new ReactiveProperty<int>();
            _mainMenu = new MainScreenManager(new MainScreenManager.Ctx
            {
                start = () => StartGame(countProductionResourceBuilding.Value),
                uiContainer = _ctx.uiContainer,
                resourceLoader = _ctx.resourceLoader,
                countProductionResourceBuilding = countProductionResourceBuilding
            });
        }

        private void StartGame(int countResourceBuildings)
        {
            if(countResourceBuildings <= 0)
                return;
            _mainMenu?.Dispose();
            _location = new Location.Location(new Location.Location.Ctx
            {
                
            });
        }

        private void OpenInventory()
        {
            _inventoryView = new InventoryManager(new InventoryManager.Ctx
            {
                availableItems = _ctx.hub.availableItems,
                availableResources = _ctx.hub.availableResource,
                close = () => _inventoryView?.Dispose(),
                resourceLoader = _ctx.resourceLoader,
                logic = _ctx.logic.Inventory,
                uiContainer = _ctx.uiContainer
            });
        }

        private void CreateWin()
        {
            _winView = new WinManager(new WinManager.Ctx
            {
                resourceLoader = _ctx.resourceLoader,
                returnToMain = ReturnToMain,
                uiContainer = _ctx.uiContainer,
            });
        }

        private void ReturnToMain()
        {
            CreateMainScreen();
        }

        protected override void OnDispose()
        {
            _mainMenu?.Dispose();
            _winView?.Dispose();
            _inventoryView?.Dispose();
            _location?.Dispose();
            base.OnDispose();
        }
    }
}