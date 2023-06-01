using System;
using Game.Production.Tools;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Production.UI
{
    internal class WinManager : BaseDisposable
    {
        private const string PREFAB_UI = "Prefabs/WinView";
        
        public struct Ctx
        {
            public IResourceLoader resourceLoader;
            public Transform uiContainer;
            public Action returnToMain;
        }

        private readonly Ctx _ctx;

        public WinManager(Ctx ctx)
        {
            _ctx = ctx;
            LoadOnScene();
        }

        private void LoadOnScene()
        {
            GameObject prefab = _ctx.resourceLoader.LoadPrefab(PREFAB_UI);
            GameObject objOnScene = AddComponent(Object.Instantiate(prefab, _ctx.uiContainer, false));
            WinView view = objOnScene.GetComponent<WinView>();
            CompositeDisposable viewDisposable = AddDispose(new CompositeDisposable());
            view.SetCtx(new WinView.Ctx
            {
                viewDisposable = viewDisposable,
                returnToMain = _ctx.returnToMain,
            });
        }
    }
}

