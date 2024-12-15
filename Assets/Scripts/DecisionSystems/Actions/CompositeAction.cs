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
            Enter();
        }

        public void RestartAction()
        {
            started = false;
            finished = false;
            _currentIndex = 0;

            foreach (IAction action in _actions)
            {
                action.RestartAction();
            }
        }

        public bool Started { get { return started; } set { started = value; } }
        public bool Finished { get { return finished; } set { finished = value; } }

        public IAction CurrentAction
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

        public void Enter()
        {
            CurrentAction = _actions[_currentIndex];
        }

        public void Exit()
        {

        }

        public void Update()
        {
            if (CurrentAction == null) return;

            if (CurrentAction.Finished)
            {
                _currentIndex++;
                if (_currentIndex >= _actions.Count)
                {
                    finished = true;
                    CurrentAction = null;
                }
                else
                {
                    CurrentAction = _actions[_currentIndex];
                }
            }

            CurrentAction?.Update();
        }

        public void FixedUpdate()
        {
            CurrentAction?.FixedUpdate();
        }
    }
}
