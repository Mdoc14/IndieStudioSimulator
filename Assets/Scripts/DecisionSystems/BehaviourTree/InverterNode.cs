using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

namespace CharactersBehaviour
{
    public class InverterNode : IBehaviourNode
    {
        private IBehaviourNode child;

        public InverterNode(IBehaviourNode child)
        {
            this.child = child;
        }

        public BehaviourState Execute()
        {
            BehaviourState state = child.Execute();
            
            if(state == BehaviourState.Success)
            {
                return BehaviourState.Failure;
            }

            if(state == BehaviourState.Failure)
            {
                return BehaviourState.Success;
            }

            return BehaviourState.Running;
        }

        public void RestartNode()
        {
            child.RestartNode();
        }
    }
}
