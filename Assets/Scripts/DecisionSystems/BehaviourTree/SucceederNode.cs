using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

namespace CharactersBehaviour
{
    public class SucceederNode : IBehaviourNode
    {
        private IBehaviourNode child;

        public SucceederNode(IBehaviourNode child)
        {
            this.child = child;
        }

        public BehaviourState Execute()
        {
            BehaviourState state = child.Execute();
            
            if (state == BehaviourState.Running)
            {
                return BehaviourState.Running;
            }
            else
            {
                return BehaviourState.Success;
            }
        }
    }
}
