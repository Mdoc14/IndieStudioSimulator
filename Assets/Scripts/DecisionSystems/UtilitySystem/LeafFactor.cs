using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class LeafFactor : IDecisionFactor
    {
        string _factorName;
        float _utility;
        float _weight;
        float _threshold;
        Func<float, float> _curve;

        public float Utility {  get { return _utility; } set { _utility = value; } }
        public float Weight { get { return _weight; } set { _weight = value; } }

        public LeafFactor(string factorName, float initialNecessity, float threshold, float weight = 1f, Func<float, float> curve = null)
        {
            _factorName = factorName;
            _utility = initialNecessity;
            _weight = weight;
            _threshold = threshold;
            _curve = curve != null ? curve : (x => x);
        }

        public bool HasUtility()
        {
            return _utility > _threshold;
        }

        public void ComputeUtility(IAgent agent)
        {
            _utility = _curve(agent.GetAgentVariable(_factorName));
            _utility = Mathf.Clamp(_utility, 0f, 1f);
        }
    }
}
