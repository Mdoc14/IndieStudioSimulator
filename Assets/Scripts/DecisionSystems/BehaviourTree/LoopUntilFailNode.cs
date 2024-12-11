using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

namespace CharactersBehaviour
{
    public class LoopUntilFailNode : IBehaviourNode
    {
        private IBehaviourNode child;
        bool hasFailed = false;

        public LoopUntilFailNode(IBehaviourNode child)
        {
            this.child = child;
        }

        public BehaviourState Execute()
        {
            if (hasFailed)
            {
                return BehaviourState.Success;
            }

            BehaviourState state = child.Execute();

            if (state == BehaviourState.Failure)
            {
                hasFailed = true;
                return BehaviourState.Success;
            }

            return state;
        }
    }
}
