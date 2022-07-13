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
        [Tooltip("0 == Roland, 1 == Vous, 2 == Rémi")]
        public int whoIsTalkingIndex;
    }
}