using System.Collections.Generic;
using UnityEngine;

namespace Game.Production.Config
{
    [CreateAssetMenu(
        fileName = "CurrencyConfig",
        menuName = "[Production]/Config/Currency")]
    internal class CurrencyConfig : ScriptableObject
    {
        public List<ContentWithIcon> AvailableCurrency;
    }
}

