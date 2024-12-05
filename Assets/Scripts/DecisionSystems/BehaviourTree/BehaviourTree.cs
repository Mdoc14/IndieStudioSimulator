using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class BehaviourTree : IBehaviourSystem
    {
        IAction _currentAction;
        public IAction Action
        {
            get { return _currentAction; }
            set
            {
                if (_currentAction != null)
                {
                    _currentAction.Exit();
                }

                _currentAction = value;

                _currentAction.Enter();
            }
        }

        IBehaviourNode _root;

        public IBehaviourNode Root { get { return _root; } set { _root = value; } }

        public BehaviourTree()
        {
        }

        public void UpdateBehaviour()
        {
            _root.Execute();
            _currentAction?.Update();
        }

        public void FixedUpdateBehaviour()
        {
            _currentAction?.FixedUpdate();
        }
    }
}
