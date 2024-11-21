using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

namespace CharactersBehaviour
{
    public class SequenceNode : IBehaviourNode
    {
        private Queue<IBehaviourNode> children;

        public SequenceNode(Queue<IBehaviourNode> behaviourNodes)
        {
            children = behaviourNodes;
        }

        public BehaviourState Execute()
        {
            while (children.Count > 0)
            {
                IBehaviourNode node = children.Dequeue();
                BehaviourState state = node.Execute();

                if(state == BehaviourState.Running)
                {
                    children.Enqueue(node);
                    return BehaviourState.Running;
                }

                if(state == BehaviourState.Failure)
                {
                    return BehaviourState.Failure;
                }
            }

            return BehaviourState.Success;
        }
    }
}
