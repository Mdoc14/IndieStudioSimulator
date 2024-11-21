using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class StateMachine : IBehaviourSystem
    {
        IState _currentState;
        public IState State 
        {  
            get { return _currentState; } 
            set 
            {
                if (_currentState != null) 
                {
                    _currentState.Exit();
                }

                _currentState = value;

                _currentState.Enter();
            } 
        }

        public StateMachine()
        {
        }

        public void UpdateBehaviour()
        {
            _currentState?.Update();
        }

        public void FixedUpdateBehaviour()
        {
            _currentState?.FixedUpdate();
        }
    }
}
