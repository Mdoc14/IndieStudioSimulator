using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class UtilitySystem : IBehaviourSystem
    {
        UtilityBasedAction _currentAction;
        public UtilityBasedAction CurrentAction
        {
            get { return _currentAction; }
            set
            {
                if (_currentAction != null)
                {
                    _currentAction.Action.Exit();
                }

                _currentAction = value;

                _currentAction?.Action.Enter();
            }
        }

        public bool activated;

        List<UtilityBasedAction> _posibleActions;

        public UtilitySystem(List<UtilityBasedAction> actions, bool activated = false)
        {
            _posibleActions = actions;
            this.activated = activated;
        }

        public float GetFactorUtility(string factorName)
        {
            foreach (UtilityBasedAction action in _posibleActions)
            {
                if (action.DecisionFactor is LeafFactor leafFactor1 && leafFactor1.FactorName.Equals(factorName))
                {
                    return leafFactor1.Utility;
                }

                if (action.DecisionFactor is FusionFactor fusionFactor)
                {
                    foreach(LeafFactor leafFactor2 in fusionFactor.GetDecisionFactors())
                    {
                        if (leafFactor2.FactorName.Equals(factorName))
                        {
                            return leafFactor2.Utility;
                        }
                    }
                }
            }

            return -1f;
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

            CurrentAction = bestAction;
        }

        void ComputeUtilities()
        {
            foreach (UtilityBasedAction action in _posibleActions)
            {
                action.DecisionFactor.ComputeUtility();
            }
        }

        public void FixedUpdateBehaviour()
        {
            if (activated)
            {
                _currentAction?.Action.FixedUpdate();
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
                else
                {
                    if (_currentAction.Action.Finished)
                    {
                        _currentAction.Reset();
                        _currentAction = null;
                        activated = false;
                        return;
                    }

                    _currentAction?.Action.Update();
                }
            }
        }
    }
}
