using System;

namespace Game.Production.Model
{
    [Serializable]
    internal class CraftItem : EntityWithCount
    {
        public EntityWithCount[] SellingCost;
    } 
}

