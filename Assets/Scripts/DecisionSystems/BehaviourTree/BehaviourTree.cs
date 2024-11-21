using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class BehaviourTree : IBehaviourSystem
    {
        IAction _actualAction;
        public IAction Action
        {
            get { return _actualAction; }
            set
            {
                if (_actualAction != null)
                {
                    _actualAction.Exit();
                }

                _actualAction = value;

                _actualAction.Enter();
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
            _actualAction?.Update();
        }

        public void FixedUpdateBehaviour()
        {
            _actualAction?.FixedUpdate();
        }
    }
}
