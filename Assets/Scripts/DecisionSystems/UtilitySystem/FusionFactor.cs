using System.Collections.Generic;

namespace CharactersBehaviour
{
    public class FusionFactor : IDecisionFactor
    {
        List<LeafFactor> decisionFactors;
        float _utility;
        float _threshold;
        float _weight;

        public float Utility { get { return _utility; } set { _utility = value; } }

        public FusionFactor(List<LeafFactor> leafFactors, float threshold, float weight = 1f)
        {
            decisionFactors = leafFactors;
            _threshold = threshold;
            _weight = weight;
            ComputeUtility();
        }

        public bool HasUtility()
        {
            return _utility >= _threshold;
        }

        public void ComputeUtility()
        {
            _utility = 0f;
            foreach (LeafFactor leafFactor in decisionFactors)
            {
                leafFactor.ComputeUtility();
                _utility += leafFactor.Utility;
            }
            _utility *= _weight;
        }

        public List<LeafFactor> GetDecisionFactors()
        {
            return new List<LeafFactor>(decisionFactors);
        }
    }
}