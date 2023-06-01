using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Production.Command
{
    internal interface IExecutorInstruction
    {
        void Apply(Logic.Logic logic);
    }  
}

