using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    private void Awake()
    {
        #region Singleton
        if (instance != null)
        {
            Debug.LogError("Plus d'une instance de DialogueManager dans la scène");
            Destroy(this);
            return;
        }
        instance = this;
        #endregion
    }
}
