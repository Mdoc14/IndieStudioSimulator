using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class LeafFactor : IDecisionFactor
    {
        IAgent _agent;
        string _factorName;

        float _minValue;
        float _maxValue;
        float _utility;

        float _weight;

        Func<float, float> _curve;

        public string FactorName { get { return _factorName; } }
        public float Utility { get { return _utility; } }

        public LeafFactor(IAgent agent, string factorName, float minValue, float maxValue, float weight = 1f, Func<float, float> curve = null)
        {
            _agent = agent;
            _factorName = factorName;
            _minValue = minValue;
            _maxValue = maxValue;

            _weight = weight;
            _curve = curve != null ? curve : (x => x);

            ComputeUtility();
        }

        public void ComputeUtility()
        {
            _utility = Mathf.Clamp(ScaleFeature(_curve(_agent.GetAgentVariable(_factorName))), 0f, 1f) * _weight;
        }

        float ScaleFeature(float data)
        {
            return (data - _minValue) / (_maxValue - _minValue);
        }
    }
}
