using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class UtilitySystem : IBehaviourSystem
    {
        UtilityBasedAction bestAction;
        List<UtilityBasedAction> posibleActions;

        void SelectBestAction()
        {
            float highestUtility = 0;
            bestAction = null;
            foreach (UtilityBasedAction action in posibleActions)
            {
                if (action.DecisionFactor.HasUtility() && (action.DecisionFactor.Utility > highestUtility))
                {
                    highestUtility = action.DecisionFactor.Utility;
                    bestAction = action;
                }
            }
        }

        public void FixedUpdateBehaviour()
        {
            bestAction?.Action.FixedUpdate();
        }

        public void UpdateBehaviour()
        {
            SelectBestAction();
            bestAction?.Action.Update();
        }
    }
}
