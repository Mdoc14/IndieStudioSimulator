using System.Collections.Generic;

namespace CharactersBehaviour
{
    public class FusionFactor : IDecisionFactor
    {
        List<LeafFactor> decisionFactors;
        float _utility;
        float _weight;

        public float Utility { get { return _utility; } }

        public FusionFactor(List<LeafFactor> leafFactors, float weight = 1f)
        {
            decisionFactors = leafFactors;
            _weight = weight;
            ComputeUtility();
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