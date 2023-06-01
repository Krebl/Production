using System.Collections.Generic;
using Game.Production.Model;
using Game.Production.Logic;
using Game.Production.Tools;
using UnityEngine;

namespace Game.Production.UI
{
    internal class GridInventory : BaseDisposable
    {
        private const string PREFAB_SHEET = "Prefabs/InventorySheet";
        
        public struct Ctx
        {
            public Transform grid;
            public IReadOnlyList<EntityWithCount> elements;
            public IReadOnlyInventoryLogic logic;
            public IResourceLoader resourceLoader;
        }

        private readonly Ctx _ctx;

        public GridInventory(Ctx ctx)
        {
            _ctx = ctx;
            foreach (var element in _ctx.elements)
            {
                EntityWithCount result;
                if (_ctx.logic.ContainResource(element.Id))
                    result = _ctx.logic.Resources[element.Id];
                else
                    result = _ctx.logic.CraftItems[element.Id];
                CreateSheet(result);
            }
        }

        private void CreateSheet(EntityWithCount entity)
        {
            GameObject prefab = _ctx.resourceLoader.LoadPrefab(PREFAB_SHEET);
            GameObject objOnScene = AddComponent(Object.Instantiate(prefab, _ctx.grid, false));
            InventorySheetView view = objOnScene.GetComponent<InventorySheetView>();
            view.SetCtx(new InventorySheetView.Ctx
            {
                resourceLoader = _ctx.resourceLoader,
                entity = entity
            });
        }
    }
}

