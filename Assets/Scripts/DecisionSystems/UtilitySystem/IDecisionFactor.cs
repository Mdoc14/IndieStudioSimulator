namespace CharactersBehaviour
{
    public interface IDecisionFactor
    {
        float Utility { get; set; }
        float Weight { get; set; }
        bool HasUtility();
        void UpdateUtility();
    }
}