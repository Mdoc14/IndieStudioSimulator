using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class BehaviourTree
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

                _actualAction.Enter();

                _actualAction = value;
            }
        }

        IBehaviourNode _root;

        public BehaviourTree(IBehaviourNode root)
        {
            _root = root;
        }

        public void Execute()
        {
            _root.Execute();
        }

        public void UpdateBehaviour()
        {
            _actualAction?.Update();
        }

        public void FixedUpdateBehaviour()
        {
            _actualAction?.FixedUpdate();
        }
    }
}
