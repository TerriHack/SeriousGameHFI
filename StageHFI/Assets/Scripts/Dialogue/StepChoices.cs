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
        public int newMood; // 0 == neutre, 1 == triste, 2 == heureux, 3 == vénère
        public Step nextStepAnswer;
    }
}