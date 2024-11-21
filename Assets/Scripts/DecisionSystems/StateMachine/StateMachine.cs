using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class StateMachine
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

        public StateMachine(IAction initialState)
        {
            _actualAction = initialState;
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
