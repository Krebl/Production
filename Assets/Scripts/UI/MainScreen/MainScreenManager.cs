using System;
using Game.Production.Tools;
using UniRx;
using UnityEngine;

namespace Game.Production.UI
{
    internal class MainScreenManager : BaseDisposable
    {
        private const string PREFAB_UI = "Prefabs/MainScreenView";
        public struct Ctx
        {
            public ReactiveProperty<int> countProductionResourceBuilding;
            public Action start;
            public Transform uiContainer;
        }

        private readonly Ctx _ctx;

        public MainScreenManager(Ctx ctx)
        {
            _ctx = ctx;
        }

        private void LoadOnScene()
        {
            
        }
    }
}