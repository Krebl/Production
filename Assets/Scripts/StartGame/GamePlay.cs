using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Production.Tools;
using Game.Production.Command;
using Game.Production.Model;
using Game.Production.Logic;
using Game.Production.Tools.Reactive;
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
            public Camera camera;
        }

        private readonly Ctx _ctx;
        private IDisposable _mainMenu;
        private IDisposable _winView;
        private IDisposable _inventoryView;
        private IDisposable _uiBuildings;

        private IDisposable _location;

        public GamePlay(Ctx ctx)
        {
            _ctx = ctx;
            HUDManager hudManager = new HUDManager(new HUDManager.Ctx
            {
                resourceLoader = _ctx.resourceLoader,
                openInventory = OpenInventory,
                uiContainer = _ctx.uiContainer,
                currency = _ctx.logic.Bank.Currency
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
            ReactiveProperty<int> countProductionResourceBuilding = new ReactiveProperty<int>(1);
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
            ReactiveEvent<string> openMarket = new ReactiveEvent<string>();
            ReactiveEvent<string> openCraft = new ReactiveEvent<string>();
            ReactiveEvent<string> openProduction = new ReactiveEvent<string>();
            _mainMenu?.Dispose();
            _location = new Location.Location(new Location.Location.Ctx
            {
                resourceLoader = _ctx.resourceLoader,
                hub = _ctx.hub,
                countProductionBuilding = countResourceBuildings,
                camera = _ctx.camera,
                openCraft = openCraft.Notify,
                openMarket = openMarket.Notify,
                openProduction = openProduction.Notify
            });
            _uiBuildings = new ManagerUIBuilding(new ManagerUIBuilding.Ctx
            {
                openCraft = openCraft,
                openMarket = openMarket,
                openProduction = openProduction,
                resourceLoader = _ctx.resourceLoader,
                commandExecuter = _ctx.commandExecuter,
                uiContainer = _ctx.uiContainer,
                logic = _ctx.logic,
                hub = _ctx.hub
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
            _uiBuildings?.Dispose();
            _inventoryView?.Dispose();
            _winView = new WinManager(new WinManager.Ctx
            {
                resourceLoader = _ctx.resourceLoader,
                returnToMain = ReturnToMain,
                uiContainer = _ctx.uiContainer,
            });
        }

        private void ReturnToMain()
        {
            _ctx.commandExecuter.Execute(new InstructionWin(new InstructionWin.Ctx()));
            _winView?.Dispose();
            _location?.Dispose();
            CreateMainScreen();
        }

        protected override void OnDispose()
        {
            _mainMenu?.Dispose();
            _winView?.Dispose();
            _inventoryView?.Dispose();
            _location?.Dispose();
            _uiBuildings?.Dispose();
            base.OnDispose();
        }
    }
}