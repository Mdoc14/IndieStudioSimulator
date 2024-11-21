using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class BehaviourTree
    {
        IAction _actualAction;
        public IAction Action {  get { return _actualAction; } set { _actualAction = value; } }

        IBehaviourNode _root;

        public void Execute()
        {
            _root.Execute();
        }

        public void UpdateBehaviour()
        {
            _actualAction.Update();
        }

        public void FixedUpdateBehaviour()
        {
            _actualAction.FixedUpdate();
        }
    }
}
