using System;
using Game.Production.Tools;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

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
            public IResourceLoader resourceLoader;
        }

        private readonly Ctx _ctx;

        public MainScreenManager(Ctx ctx)
        {
            _ctx = ctx;
            LoadOnScene();
        }

        private void LoadOnScene()
        {
            GameObject prefab = _ctx.resourceLoader.LoadPrefab(PREFAB_UI);
            GameObject objOnScene = AddComponent(Object.Instantiate(prefab, _ctx.uiContainer, false));
            MainScreenView view = objOnScene.GetComponent<MainScreenView>();
            CompositeDisposable viewDisposable = AddDispose(new CompositeDisposable());
            
            view.SetCtx(new MainScreenView.Ctx
            {
                viewDisposable = viewDisposable,
                countProductionResourceBuilding = _ctx.countProductionResourceBuilding,
                startGame = _ctx.start
            });
        }
    }
}