using System;

namespace Game.Production.Config
{
    [Serializable]
    internal class CraftItemContent : ContentWithIcon
    {
        public CostContent[] SellingCost;
    } 
}

