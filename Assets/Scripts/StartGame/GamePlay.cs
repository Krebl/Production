using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Production.Tools;
using Game.Production.Command;
using Game.Production.Model;
using Game.Production.Logic;

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

        public GamePlay(Ctx ctx)
        {
            _ctx = ctx;
        }
    }
}