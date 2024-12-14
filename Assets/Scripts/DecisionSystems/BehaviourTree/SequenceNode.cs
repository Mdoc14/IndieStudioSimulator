using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

namespace CharactersBehaviour
{
    public class SequenceNode : IBehaviourNode
    {
        private List<IBehaviourNode> children;

        public SequenceNode(List<IBehaviourNode> behaviourNodes)
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

                if(state == BehaviourState.Failure)
                {
                    return BehaviourState.Failure;
                }
            }

            return BehaviourState.Success;
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
