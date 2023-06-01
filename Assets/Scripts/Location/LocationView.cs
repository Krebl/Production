using UnityEngine;
using Game.Production.Tools;

namespace Game.Production.Location
{
    internal class LocationView : BaseMonoBehaviour
    {
        public struct Ctx
        {
            
        }

        [SerializeField] private Transform[] _spawnPointForProduction;
        [SerializeField] private Transform[] _spawnPointForCraft;
        [SerializeField] private Transform[] _spawnPointMarkets;

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
        }

        public Transform[] SpawnPointForProduction => _spawnPointForProduction;
        public Transform[] SpawnPointForCraft => _spawnPointForCraft;
        public Transform[] SpawnPointMarkets => _spawnPointMarkets;
    }
}

