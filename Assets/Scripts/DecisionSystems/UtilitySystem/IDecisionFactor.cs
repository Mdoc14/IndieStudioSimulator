namespace CharactersBehaviour
{
    public interface IDecisionFactor
    {
        float Utility { get; set; }
        bool HasUtility();
        void ComputeUtility();
    }
}