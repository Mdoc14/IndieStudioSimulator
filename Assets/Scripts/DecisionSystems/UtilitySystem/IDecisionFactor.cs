namespace CharactersBehaviour
{
    public interface IDecisionFactor
    {
        float Utility { get; }
        void ComputeUtility();
    }
}