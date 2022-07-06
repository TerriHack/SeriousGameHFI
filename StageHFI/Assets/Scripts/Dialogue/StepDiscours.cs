using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "StepDiscours_", menuName = "Discours/Discours", order = 1)]
    public class StepDiscours : Step
    {
        public int whoIsTalkingIndex;
        public bool isUnlockingTheTablet;
    }
}