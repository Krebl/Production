using System;
using UnityEngine;
using Game.Production.Tools;
using Game.Production.Model;
using Game.Production.Tools.Reactive;
using Object = UnityEngine.Object;

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
            public Camera camera;
            public Action<string> openMarket;
            public Action<string> openProduction;
            public Action<string> openCraft;
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

            ReactiveEvent<RaycastHit> clicked = new ReactiveEvent<RaycastHit>();
            if (view.SpawnPointMarkets.Length > 0)
            {
                BuildingOnScene market = new BuildingOnScene(new BuildingOnScene.Ctx
                {
                    building = _ctx.hub.markets[0],
                    resourceLoader = _ctx.resourceLoader,
                    point = view.SpawnPointMarkets[0],
                    clicked = clicked,
                    openView = _ctx.openMarket
                });
                AddDispose(market);
            }
            if (view.SpawnPointForCraft.Length > 0)
            {
                BuildingOnScene craft = new BuildingOnScene(new BuildingOnScene.Ctx
                {
                    building = _ctx.hub.craftItemBuildings[0],
                    resourceLoader = _ctx.resourceLoader,
                    point = view.SpawnPointForCraft[0],
                    clicked = clicked,
                    openView = _ctx.openCraft
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
                    point = view.SpawnPointForProduction[i],
                    clicked = clicked,
                    openView = _ctx.openProduction
                });
                AddDispose(production);
            }

            ClickListener clickListener = view.GetComponent<ClickListener>();
            clickListener.SetCtx(new ClickListener.Ctx
            {
                camera = _ctx.camera,
                clicked = clicked
            });
        }
    }
}

