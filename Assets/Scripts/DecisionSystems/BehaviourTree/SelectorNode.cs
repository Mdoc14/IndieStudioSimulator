using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

namespace CharactersBehaviour
{
    public class SelectorNode : IBehaviourNode
    {
        private Queue<IBehaviourNode> children;
        private Queue<IBehaviourNode> backup;

        public SelectorNode(Queue<IBehaviourNode> behaviourNodes)
        {
            children = behaviourNodes;
            backup = new Queue<IBehaviourNode>(behaviourNodes);
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
                }

                if(state == BehaviourState.Success)
                {
                    RestartNode();
                    return BehaviourState.Success;
                }
            }

            RestartNode();
            return BehaviourState.Failure;
        }

        public void RestartNode()
        {
            children = backup;
        }
    }
}
