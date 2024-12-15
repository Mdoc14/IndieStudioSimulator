using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class ActionNode : IBehaviourNode
    {
        private IAction action;
        private BehaviourTree tree;

        public ActionNode(IAction action, BehaviourTree tree)
        {
            this.action = action;
            this.tree = tree;
        }

        public BehaviourState Execute()
        {
            if (!action.Started)
            {
                tree.Action = action;
            }

            if (action.Finished)
            {
                if (tree.Action == action) tree.Action = null;
                return BehaviourState.Success;
            }
            else
            {
                return BehaviourState.Running;
            }
        }

        public void RestartNode()
        {
            action.RestartAction();
        }
    }
}
