using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public abstract class ActionNode : IBehaviourNode
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
            if (!action.FirstTimeExecuted)
            {
                tree.Action = action;
                action.Enter();
            }

            if (action.HasFinished)
            {
                action.Exit();
                return BehaviourState.Success;
            }
            else
            {
                return BehaviourState.Running;
            }
        }
    }
}
