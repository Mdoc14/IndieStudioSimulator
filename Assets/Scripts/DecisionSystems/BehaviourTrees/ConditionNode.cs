using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class ConditionNode : IBehaviourNode
    {
        private Func<bool> condition;

        public ConditionNode(Func<bool> condition)
        {
            this.condition = condition;
        }

        public BehaviourState Execute()
        {
            return condition()? BehaviourState.Success: BehaviourState.Failure;
        }
    }
}
