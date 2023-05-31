using System;
using System.Collections.Generic;

namespace Game.Production.Model
{
    [Serializable]
    internal class CraftItem : EntityWithCount
    {
        public List<EntityWithCount> SellingCost;
    } 
}

