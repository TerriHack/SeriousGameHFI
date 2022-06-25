using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "StepDiscours_", menuName = "Discours/Information", order = 2)]
    public class StepInformation : Step
    {
        public int trustCost;
        public Step nextStep;
    }
}