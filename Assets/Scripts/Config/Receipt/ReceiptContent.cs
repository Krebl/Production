using System;

namespace Game.Production.Config
{
    [Serializable]
    internal class ReceiptContent : Content
    {
        public CostContent[] CostCraft;
        public string IdResult;
    }
}

