using System;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public abstract class Step : ScriptableObject
    {
        [TextArea] 
        public string message;
        public Step nextStep;
        public Sprite newBackground;
        public int whoIsTalkingIndex; // 0 == roland, 1 == vous, 2 == rémi
    }
}