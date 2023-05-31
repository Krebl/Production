using System;
using UnityEngine;
using Game.Production.Config;
using Game.Production.Converters;
using Game.Production.Logic;
using Game.Production.Model;
using UniRx;

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
            
            Logic.Logic logic = new Logic.Logic
            {
                win = winLogic
            };
        }
    }
}

