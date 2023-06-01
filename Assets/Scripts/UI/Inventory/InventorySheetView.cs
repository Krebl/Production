using Game.Production.Model;
using Game.Production.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Production.UI
{
    internal class InventorySheetView : BaseMonoBehaviour
    {
        public struct Ctx
        {
            public EntityWithCount entity;
            public IResourceLoader resourceLoader;
        }
        
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI labelName;
        [SerializeField] private TextMeshProUGUI labelCount;

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            icon.sprite = _ctx.resourceLoader.LoadSprite(_ctx.entity.IconPath);
            labelCount.text = _ctx.entity.Count.ToString();
            labelName.text = _ctx.entity.Name;
        }
    }
}

