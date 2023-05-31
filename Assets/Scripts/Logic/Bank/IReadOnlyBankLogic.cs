using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Production.Logic
{
    internal interface IReadOnlyBankLogic
    {
        bool EnoughCurrency(string id, int count);
    }
}

