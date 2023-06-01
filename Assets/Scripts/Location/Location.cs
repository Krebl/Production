using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Production.Tools;

namespace Game.Production.Location
{
    internal class Location : BaseDisposable
    {
        public struct Ctx
        {
            
        }

        private readonly Ctx _ctx;

        public Location(Ctx ctx)
        {
            _ctx = ctx;
        }
    }
}

