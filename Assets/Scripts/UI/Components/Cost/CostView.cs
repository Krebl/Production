using UnityEngine;
using Game.Production.Tools;
using Game.Production.Model;
using TMPro;
using UniRx;
using UnityEngine.UI;

namespace Game.Production.UI
{
    internal class CostView : MonoBehaviour
    {
        public struct Ctx
        {
            public IResourceLoader resourceLoader;
            public IReadOnlyReactiveProperty<EntityWithCount> cost;
            public CompositeDisposable viewDisposable;
        }

        [SerializeField] private Image _iconCost;
        [SerializeField] private TextMeshProUGUI _labelCost;

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            _ctx.cost.Subscribe(cost =>
            {
                _iconCost.sprite = _ctx.resourceLoader.LoadSprite(cost.IconPath);
                _labelCost.text = cost.Count.ToString();
            }).AddTo(_ctx.viewDisposable);
        }
    } 
}

