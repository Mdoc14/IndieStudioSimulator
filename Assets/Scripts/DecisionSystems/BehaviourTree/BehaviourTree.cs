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

                _currentAction?.Enter();
            }
        }

        IBehaviourNode _root;
        public IBehaviourNode Root { get { return _root; } set { _root = value; } }

        BehaviourState _state = BehaviourState.Running;
        public BehaviourState State { get { return _state; } }

        public BehaviourTree()
        {
        }

        public void UpdateBehaviour()
        {
            if (_state == BehaviourState.Running) {
                _state = _root.Execute();
            }
            _currentAction?.Update();
        }

        public void FixedUpdateBehaviour()
        {
            _currentAction?.FixedUpdate();
        }
    }
}
