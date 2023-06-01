using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Production.Model;


namespace Game.Production.Command
{
    internal class InstructionProductionResource : IInstruction, IExecutorInstruction
    {
        public struct Ctx
        {
            
        }

        private readonly Ctx _ctx;

        public InstructionProductionResource(Ctx ctx)
        {
            _ctx = ctx;
        }
        
        public void Apply(Logic.Logic logic)
        {

        }
    }
}

