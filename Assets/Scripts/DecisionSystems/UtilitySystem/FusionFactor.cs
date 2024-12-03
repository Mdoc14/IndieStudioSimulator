using System.Collections.Generic;

namespace CharactersBehaviour
{
    public class FusionFactor : IDecisionFactor
    {
        List<LeafFactor> decisionFactors;
        float _utility;
        float _weight;

        public float Utility { get { return _utility; } set { _utility = value; } }
        public float Weight { get { return _weight; } set { _weight = value; } }

        public FusionFactor(List<LeafFactor> leafFactors)
        {
            decisionFactors = leafFactors;
            _utility = 0f;
            foreach (LeafFactor leafFactor in decisionFactors)
            {
                _utility += leafFactor.Weight * leafFactor.Utility;
            }
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

        public void UpdateUtility()
        {
            _utility = 0f;
            foreach (LeafFactor leafFactor in decisionFactors)
            {
                _utility += leafFactor.Weight * leafFactor.Utility;
            }
        }
    }
}