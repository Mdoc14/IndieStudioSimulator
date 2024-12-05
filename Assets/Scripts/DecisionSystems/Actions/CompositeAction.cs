using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class CompositeAction : IAction
    {
        List<IAction> _actions;
        IAction _currentAction;

        int _currentIndex = 0;

        protected bool started = false;
        protected bool finished = false;

        public CompositeAction(List<IAction> actions)
        {
            _actions = actions;
        }

        public bool HasStarted { get { return started; } }
        public bool HasFinished { get { return finished; } }

        public void Enter()
        {
            _currentAction = _actions[_currentIndex];
        }

        public void Exit()
        {

        }

        public void Update()
        {
            if (_currentAction.HasFinished)
            {
                _currentIndex++;
                if (_currentIndex >= _actions.Count)
                {
                    finished = true;
                    _currentAction = null;
                }
                else
                {
                    _currentAction = _actions[_currentIndex];
                }
            }

            _currentAction?.Update();
        }

        public void FixedUpdate()
        {
            _currentAction?.FixedUpdate();
        }
    }
}
