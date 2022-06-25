using System;
using TMPro;
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
        public Step nextStepAnswer;
    }
}