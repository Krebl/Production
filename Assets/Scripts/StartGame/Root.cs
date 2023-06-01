using System;
using UnityEngine;
using Game.Production.Config;
using Game.Production.Converters;
using Game.Production.Logic;
using Game.Production.Model;
using UniRx;
using Game.Production.Tools;
using Game.Production.Command;

namespace Game.Production.Start
{
    internal class Root : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private Transform _uiContainer;

        public void Start()
        {
            
            Converters.Converters converters = new Converters.Converters(new Converters.Converters.Ctx
            {
                gameConfig = _gameConfig
            });

           Hub hub = converters.ConvertToHub();
           ReactiveDictionary<string, EntityWithCount> currency = new ReactiveDictionary<string, EntityWithCount>();
           WinLogic winLogic = new WinLogic(new WinLogic.Ctx
           {
               currency = currency,
           });
           InventoryLogic inventoryLogic = new InventoryLogic(new InventoryLogic.Ctx());
           BankLogic bankLogic = new BankLogic(new BankLogic.Ctx
           {
               availableCurrency = hub.availableCurrency,
               currency = currency
           });
           ProductionLogic production = new ProductionLogic(new ProductionLogic.Ctx());
           CraftItemLogic craftItemLogic = new CraftItemLogic(new CraftItemLogic.Ctx
           {
               receipts = hub.availableReceipts
           });
            
            Logic.Logic logic = new Logic.Logic
            {
                win = winLogic,
                inventory = inventoryLogic,
                bank = bankLogic,
                production = production,
                craftItem = craftItemLogic
            };

            IResourceLoader resourceLoader = new ResourceLoader();
            ICommandExecuter commandExecuter = new CommandExecuter(new CommandExecuter.Ctx
            {
                logic = logic
            });

            GamePlay gamePlay = new GamePlay(new GamePlay.Ctx
            {
                resourceLoader = resourceLoader,
                commandExecuter = commandExecuter,
                logic = logic,
                uiContainer = _uiContainer,
                hub = hub
            });
        }
    }
}

