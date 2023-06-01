using UnityEngine;
using Game.Production.Tools;
using Game.Production.Model;

namespace Game.Production.Location
{
    internal class Location : BaseDisposable
    {
        private const string PREFAB = "Prefabs/Location";
        public struct Ctx
        {
            public IResourceLoader resourceLoader;
            public int countProductionBuilding;
            public Hub hub;
        }

        private readonly Ctx _ctx;

        public Location(Ctx ctx)
        {
            _ctx = ctx;
            GameObject prefab = _ctx.resourceLoader.LoadPrefab(PREFAB);
            GameObject objOnScene = AddComponent(Object.Instantiate(prefab, null, false));
            objOnScene.transform.localPosition = Vector3.zero;
            LocationView view = objOnScene.GetComponent<LocationView>();
            view.SetCtx(new LocationView.Ctx());

            if (view.SpawnPointMarkets.Length > 0)
            {
                BuildingOnScene market = new BuildingOnScene(new BuildingOnScene.Ctx
                {
                    building = _ctx.hub.markets[0],
                    resourceLoader = _ctx.resourceLoader,
                    point = view.SpawnPointMarkets[0]
                });
                AddDispose(market);
            }
            if (view.SpawnPointForCraft.Length > 0)
            {
                BuildingOnScene craft = new BuildingOnScene(new BuildingOnScene.Ctx
                {
                    building = _ctx.hub.craftItemBuildings[0],
                    resourceLoader = _ctx.resourceLoader,
                    point = view.SpawnPointForCraft[0]
                });
                AddDispose(craft);
            }

            for (int i = 0; i < _ctx.countProductionBuilding; i++)
            {
                if(i >= view.SpawnPointForProduction.Length || i >= _ctx.hub.productionResourceBuildings.Count)
                    break;
                BuildingOnScene production = new BuildingOnScene(new BuildingOnScene.Ctx
                {
                    building = _ctx.hub.productionResourceBuildings[i],
                    resourceLoader = _ctx.resourceLoader,
                    point = view.SpawnPointForProduction[i]
                });
                AddDispose(production);
            }
        }
    }
}

