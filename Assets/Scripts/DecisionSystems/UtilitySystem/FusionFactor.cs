using System.Collections.Generic;

namespace CharactersBehaviour
{
    public class FusionFactor : IDecisionFactor
    {
        List<LeafFactor> decisionFactors;
        float _utility;
        float _weight;

        public float Utility { get { return _utility; } set { _utility = value; } }

        public FusionFactor(List<LeafFactor> leafFactors, float weight)
        {
            decisionFactors = leafFactors;
            _weight = weight;
            ComputeUtility();
        }

        public bool HasUtility()
        {
            foreach (LeafFactor leafFactor in decisionFactors)
            {
                if (!leafFactor.HasUtility())
                {
                    return false;
                }
            }

            return true;
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
    }
}