using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

namespace CharactersBehaviour
{
    public class LoopNNode : IBehaviourNode
    {
        private IBehaviourNode child;
        int currentIteration = 0;
        int maxN;

        public LoopNNode(IBehaviourNode child, int n)
        {
            this.child = child;
            this.maxN = n;
        }

        public BehaviourState Execute()
        {
            if (maxN == 0)
            {
                BehaviourState state = child.Execute();

                if (state == BehaviourState.Success || state == BehaviourState.Failure)
                {
                    child.RestartNode();
                }

                return BehaviourState.Running;
            }

            if (currentIteration < maxN)
            {
                BehaviourState state = child.Execute();

                if (state == BehaviourState.Success || state == BehaviourState.Failure)
                {
                    currentIteration++;
                    child.RestartNode();
                }

                return BehaviourState.Running;
            }
            else
            {
                return BehaviourState.Success;
            }
        }

        public void RestartNode()
        {
            currentIteration = 0;
            child.RestartNode();
        }
    }
}
