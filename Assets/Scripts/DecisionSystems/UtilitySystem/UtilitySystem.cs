using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class UtilitySystem : IBehaviourSystem
    {
        UtilityBasedAction _bestAction;
        List<UtilityBasedAction> _posibleActions;
        IAgent _agent;

        public UtilitySystem(List<UtilityBasedAction> actions, IAgent agent)
        {
            _posibleActions = actions;
            _agent = agent;
        }

        void SelectBestAction()
        {
            float highestUtility = 0;
            _bestAction = null;
            foreach (UtilityBasedAction action in _posibleActions)
            {
                action.DecisionFactor.ComputeUtility(_agent);

                if (action.DecisionFactor.HasUtility() && (action.DecisionFactor.Utility > highestUtility))
                {
                    highestUtility = action.DecisionFactor.Utility;
                    _bestAction = action;
                }
            }
        }

        public void FixedUpdateBehaviour()
        {
            _bestAction?.Action.FixedUpdate();
        }

        public void UpdateBehaviour()
        {
            SelectBestAction();
            _bestAction?.Action.Update();
        }
    }
}
