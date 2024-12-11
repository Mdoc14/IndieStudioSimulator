using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class UtilitySystem : IBehaviourSystem
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

        public bool activated;

        List<UtilityBasedAction> _posibleActions;
        IAgent _agent;

        public UtilitySystem(List<UtilityBasedAction> actions, IAgent agent, bool activated = false)
        {
            _posibleActions = actions;
            _agent = agent;
            this.activated = activated;
        }

        void SelectBestAction()
        {
            float highestUtility = 0;
            UtilityBasedAction bestAction = null;

            foreach (UtilityBasedAction action in _posibleActions)
            {
                if (action.DecisionFactor.HasUtility() && (action.DecisionFactor.Utility > highestUtility))
                {
                    highestUtility = action.DecisionFactor.Utility;
                    bestAction = action;
                }
            }

            _currentAction = bestAction?.Action;
        }

        void ComputeUtilities()
        {
            foreach (UtilityBasedAction action in _posibleActions)
            {
                action.DecisionFactor.ComputeUtility(_agent);
            }
        }

        public void FixedUpdateBehaviour()
        {
            if (activated)
            {
                _currentAction?.FixedUpdate();
            }
        }

        public void UpdateBehaviour()
        {
            ComputeUtilities();

            if (activated)
            {
                if (_currentAction == null)
                {
                    SelectBestAction();
                    if (_currentAction == null) activated = false;
                }

                if (_currentAction.HasFinished)
                {
                    _currentAction = null;
                    activated = false;
                }

                _currentAction?.Update();
            }
        }
    }
}
