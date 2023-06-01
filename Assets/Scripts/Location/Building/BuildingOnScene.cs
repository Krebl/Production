using UnityEngine;
using Game.Production.Tools;
using Game.Production.Model;

namespace Game.Production.Location
{
    internal class BuildingOnScene : BaseDisposable
    {
        public struct Ctx
        {
            public Building building;
            public IResourceLoader resourceLoader;
            public Transform point;
        }

        private readonly Ctx _ctx;

        public BuildingOnScene(Ctx ctx)
        {
            _ctx = ctx;
            GameObject prefab = _ctx.resourceLoader.LoadPrefab(_ctx.building.PrefabPath);
            GameObject objOnScene = AddComponent(Object.Instantiate(prefab, _ctx.point, false));
            objOnScene.transform.localPosition = Vector3.zero;
        }
    }
}

