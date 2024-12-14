using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

namespace CharactersBehaviour
{
    public class SelectorNode : IBehaviourNode
    {
        private List<IBehaviourNode> children;

        public SelectorNode(List<IBehaviourNode> behaviourNodes)
        {
            children = behaviourNodes;
        }

        public BehaviourState Execute()
        {
            foreach (IBehaviourNode node in children)
            {
                BehaviourState state = node.Execute();

                if(state == BehaviourState.Running)
                {
                    return BehaviourState.Running;
                }

                if(state == BehaviourState.Success)
                {
                    return BehaviourState.Success;
                }
            }

            return BehaviourState.Failure;
        }

        public void RestartNode()
        {
            foreach (IBehaviourNode node in children)
            {
                node.RestartNode();
            }
        }
    }
}
