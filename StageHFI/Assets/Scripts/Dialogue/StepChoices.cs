using System;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "StepDiscours_", menuName = "Discours/Choices", order = 3)]
    public class StepChoices : Step
    {
        public StepChoice stepChoice1;
        public StepChoice stepChoice2;
        public StepChoice stepChoice3;
    }
    
    [Serializable]
    public struct StepChoice
    {
        public string choiceText;
        [TextArea]
        public string clientResponse;
        public bool isGoodAnswer;
        public bool isBadAnswer;
        public Step nextStepAnswer;
        public int newMood; // 1 == Heureux, 2 == triste / Pensif, 3 == vénère / Rassurant, 4 == neutre, 5 == neutre_2
    }
}