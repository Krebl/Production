using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Production.Tools;
using Game.Production.Command;
using Game.Production.Model;
using Game.Production.Logic;
using Game.Production.UI;

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

        public GamePlay(Ctx ctx)
        {
            _ctx = ctx;
            _mainMenu = new MainScreenManager(new MainScreenManager.Ctx
            {
                
            });
        }

        protected override void OnDispose()
        {
            _mainMenu?.Dispose();
            base.OnDispose();
        }
    }
}