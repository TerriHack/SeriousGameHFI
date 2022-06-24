using UnityEngine;

public class TrustManager : MonoBehaviour
{
   public static TrustManager instance;
   
   public int trust;

   private void Awake()
   {
       #region Singleton
       if (instance != null)
       {
           Debug.LogError("Plus d'une instance de TrustManager dans la scène");
           Destroy(this);
           return;
       }
       instance = this;
       #endregion
   }
}
