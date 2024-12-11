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
                return child.Execute();
            }

            if (currentIteration < maxN)
            {
                BehaviourState state = child.Execute();

                if (state == BehaviourState.Success || state == BehaviourState.Failure)
                {
                    currentIteration++;
                }

                return state;
            }
            else
            {
                return BehaviourState.Success;
            }
        }
    }
}
