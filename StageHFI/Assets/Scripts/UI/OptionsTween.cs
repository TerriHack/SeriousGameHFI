using UnityEngine;

namespace UI
{
    public class OptionsTween : MonoBehaviour
    {
        [Header("Animation Curves")]
        public LeanTweenType curveIn;
        public LeanTweenType curveOut;
    
        [Space]
        [SerializeField] private RectTransform background;
    
        private void Start()
        {
            // Reset Scale
            transform.localScale = Vector3.zero;
        
            // Reset scale & alpha
//            LeanTween.scale(background, Vector3.zero, 0);
  //          LeanTween.alpha(background, 0, 0);
        }

        public void OpenOptions()
        {
            LeanTween.scale(gameObject,Vector3.one, 0.4f).setEase(curveIn);
        
            // Make the background appear (scale & alpha)
      //     LeanTween.scale(background, Vector3.one, 0);
      //      LeanTween.alpha(background, 0.4f, 0.4f).setEase(curveIn);
        }
    
        public void CloseOptions()
        {
            LeanTween.scale(gameObject,Vector3.zero, 0.4f).setEase(curveOut);
        
            // Make the background disappear (scale & alpha)
      //      LeanTween.scale(background, Vector3.zero, 0);
      //      LeanTween.alpha(background, 0, 0.4f).setEase(curveOut);
        }
    }
}
