using UnityEngine;
using Game.Production.Tools;
using Game.Production.Model;
using Game.Production.Tools.Reactive;

namespace Game.Production.Location
{
    internal class BuildingOnScene : BaseDisposable
    {
        public struct Ctx
        {
            public Building building;
            public IResourceLoader resourceLoader;
            public Transform point;
            public IReadOnlyReactiveEvent<RaycastHit> clicked;
        }

        private readonly Ctx _ctx;

        public BuildingOnScene(Ctx ctx)
        {
            _ctx = ctx;
            GameObject prefab = _ctx.resourceLoader.LoadPrefab(_ctx.building.PrefabPath);
            GameObject objOnScene = AddComponent(Object.Instantiate(prefab, _ctx.point, false));
            objOnScene.transform.localPosition = Vector3.zero;
            BuildingOnSceneView view = objOnScene.GetComponent<BuildingOnSceneView>();
            view.SetCtx(new BuildingOnSceneView.Ctx
            {
                id = _ctx.building.Id
            });
            AddDispose(_ctx.clicked.SubscribeWithSkip(hit =>
            {
                BuildingOnSceneView checkedBuild = hit.transform.GetComponent<BuildingOnSceneView>();
                if (checkedBuild != null && checkedBuild.Id == _ctx.building.Id)
                {
                    Debug.Log("CLICKED");
                }
            }));
        }
    }
}

