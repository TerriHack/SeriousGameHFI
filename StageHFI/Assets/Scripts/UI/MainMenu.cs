using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Animation Curves")]
    public LeanTweenType curveIn;
    public LeanTweenType curveOut;

    [Space]
    [SerializeField]
    private GameObject quitPopUp;
    [SerializeField]
    private RectTransform background;
    
    private void Start()
    {
        // Reset Scale
        quitPopUp.transform.localScale = Vector3.zero;
        
        // Reset scale & alpha
        LeanTween.scale(background, Vector3.zero, 0);
        LeanTween.alpha(background, 0, 0);
    }
    
    public void StartGame() => SceneManager.LoadScene("MainScene");
    
    public void OpenQuitPopUp()
    {
        LeanTween.scale(quitPopUp,Vector3.one, 0.4f).setEase(curveIn);
        
        // Make the background appear (scale & alpha)
        LeanTween.scale(background, Vector3.one, 0);
        LeanTween.alpha(background, 0.4f, 0.4f).setEase(curveIn);
    }
    
    public void CloseQuitPopUp()
    {
        LeanTween.scale(quitPopUp,Vector3.zero, 0.4f).setEase(curveIn);
        
        // Make the background disappear (scale & alpha)
        LeanTween.scale(background, Vector3.zero, 0);
        LeanTween.alpha(background, 0, 0.4f).setEase(curveIn);
    }

    public void QuitGame() => Application.Quit();
}
