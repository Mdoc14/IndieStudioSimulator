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
        float _threshold;
        float _weight;

        Func<float, float> _curve;

        public float Utility {  get { return _utility; } set { _utility = value; } }

        public LeafFactor(IAgent agent, string factorName, float minValue, float maxValue, float threshold, float weight = 1f, Func<float, float> curve = null)
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
            _utility = _curve(ScaleFeature(_agent.GetAgentVariable(_factorName)));
            _utility = Mathf.Clamp(_utility, 0f, 1f) * _weight;
        }

        float ScaleFeature(float data)
        {
            return (data - _minValue) / (_maxValue - _minValue);
        }
    }
}
