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
        [Tooltip("0 == ne change pas, 1 == Heureux, 2 == triste / Pensif, 3 == vénère / Rassurant, 4 == neutre, 5 == neutre_2")]
        public int newMood; 
    }
}