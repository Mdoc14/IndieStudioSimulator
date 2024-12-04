using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class UtilityBasedAction
    {
        IAction _action;
        IDecisionFactor _decisionFactor;

        public IAction Action
        {
            get { return _action; }
            set
            {
                _action = value;
            }
        }
        public IDecisionFactor DecisionFactor
        {
            get { return _decisionFactor; }
            set
            {
                _decisionFactor = value;
            }
        }

        public UtilityBasedAction(IAction action, IDecisionFactor decisionFactor)
        {
            _action = action;
            _decisionFactor = decisionFactor;
        }
    }
}
