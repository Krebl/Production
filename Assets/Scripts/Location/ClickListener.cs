using UnityEngine;
using Game.Production.Tools;
using Game.Production.Tools.Reactive;
using UnityEngine.EventSystems;

namespace Game.Production.Location
{
    internal class ClickListener : BaseMonoBehaviour
    {
        public struct Ctx
        {
            public ReactiveEvent<RaycastHit> clicked;
            public Camera camera;
        }

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
        }

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() )
            {
                Vector3 positionClick = Input.mousePosition;
                Ray ray = _ctx.camera.ScreenPointToRay(positionClick);
                if (Physics.Raycast(ray, out var hitInfo))
                {
                    _ctx.clicked?.Notify(hitInfo);
                }
            }
        }
    }
}

