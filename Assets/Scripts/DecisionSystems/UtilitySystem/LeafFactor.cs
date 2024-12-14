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

        Func<float, float> _curve;

        float _threshold;
        float _weight;

        public string FactorName { get { return _factorName; } }
        public float Utility { get { return _utility; } set { _utility = value; } }

        public LeafFactor(IAgent agent, string factorName, float minValue, float maxValue, Func<float, float> curve = null, float threshold = 0f, float weight = 1f)
        {
            _agent = agent;
            _factorName = factorName;
            _minValue = minValue;
            _maxValue = maxValue;

            _threshold = threshold;
            _weight = weight;
            _curve = curve != null ? curve : (x => x);

            ComputeUtility();
        }

        public bool HasUtility()
        {
            return _utility >= _threshold;
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
