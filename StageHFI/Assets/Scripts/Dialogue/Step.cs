using System;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public abstract class Step : ScriptableObject
    {
        [TextArea] 
        public string message;
    }
}