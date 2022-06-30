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
        public Sprite newMood;
        public Sprite newBackground;
    }
}