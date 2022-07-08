using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Animation Curves")]
        public LeanTweenType curveIn;
        public LeanTweenType curveOut;

        [Space]
        [SerializeField] private GameObject quitPopUp;
        [SerializeField] private GameObject creditsPopUp;

     //   [SerializeField] private RectTransform background;
        [SerializeField] private RectTransform characters;

        private void Start()
        {
            quitPopUp.transform.localScale = Vector3.zero;
            creditsPopUp.transform.localScale = Vector3.zero;
        }

        public void StartGame() => StartCoroutine(PlayStartAnimation());

        private IEnumerator PlayStartAnimation()
        {
            LeanTween.alpha(characters, 0, 1);
            gameObject.LeanMoveX(-500, 2f).setEase(curveOut);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("MainScene");
        }

        public void OpenQuitPopUp()
        {
            LeanTween.scale(quitPopUp,Vector3.one, 0.4f).setEase(curveIn);
        
            // Make the background appear (scale & alpha)
            /*LeanTween.scale(background, Vector3.one, 0);
            LeanTween.alpha(background, 0.4f, 0.4f).setEase(curveIn);*/
        }
    
        public void CloseQuitPopUp()
        {
            LeanTween.scale(quitPopUp,Vector3.zero, 0.4f).setEase(curveIn);
        
            // Make the background disappear (scale & alpha)
            /*LeanTween.scale(background, Vector3.zero, 0);
            LeanTween.alpha(background, 0, 0.4f).setEase(curveIn);*/
        }
        
        public void OpenCreditsPopUp()
        {
            LeanTween.scale(creditsPopUp,Vector3.one, 0.4f).setEase(curveIn);
        
            // Make the background appear (scale & alpha)
            /*LeanTween.scale(background, Vector3.one, 0);
            LeanTween.alpha(background, 0.4f, 0.4f).setEase(curveIn);*/
        }
    
        public void CloseCreditsPopUp()
        {
            LeanTween.scale(creditsPopUp,Vector3.zero, 0.4f).setEase(curveIn);
        
            // Make the background disappear (scale & alpha)
            /*LeanTween.scale(background, Vector3.zero, 0);
            LeanTween.alpha(background, 0, 0.4f).setEase(curveIn);*/
        }

        public void QuitGame() => Application.Quit();
    }
}
