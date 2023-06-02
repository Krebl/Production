using Game.Production.Tools;

namespace Game.Production.Location
{
    internal class BuildingOnSceneView : BaseMonoBehaviour
    {
        public struct Ctx
        {
            public string id;
        }

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
        }

        public string Id => _ctx.id;
    }
}

