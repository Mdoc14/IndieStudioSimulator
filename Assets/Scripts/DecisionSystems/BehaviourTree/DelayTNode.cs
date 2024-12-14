using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

namespace CharactersBehaviour
{
    public class DelayTNode : IBehaviourNode
    {
        private IBehaviourNode child;
        float maxTime;
        float timer;

        public DelayTNode(IBehaviourNode child, float time)
        {
            timer = 0f;
            this.child = child;
            this.maxTime = time;
        }

        public BehaviourState Execute()
        {
            if (timer >= maxTime)
            {
                return child.Execute();
            }

            timer += Time.deltaTime;
            return BehaviourState.Running;
        }

        public void RestartNode()
        {
            timer = 0f;
            child.RestartNode();
        }
    }
}
