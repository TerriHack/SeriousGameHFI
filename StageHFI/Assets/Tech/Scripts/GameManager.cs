using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        #region Singleton
        if (instance != null)
        {
            Debug.LogError("Plus d'une instance de GameManager dans la scène");
            Destroy(this);
            return;
        }
        instance = this;
        #endregion
    }
}
