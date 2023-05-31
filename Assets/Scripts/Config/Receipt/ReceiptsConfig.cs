using System.Collections.Generic;
using UnityEngine;

namespace Game.Production.Config
{
    [CreateAssetMenu(
        fileName = "ReceiptConfig",
        menuName = "[Production]/Config/Receipts")]
    internal class ReceiptsConfig : ScriptableObject
    {
        public List<ReceiptContent> AvailableReceipts;
    }
}

