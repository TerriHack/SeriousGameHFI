using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject[] choiceBtn;
    public TextMeshProUGUI[] choiceText;

    public TextMeshProUGUI dialogueText;

    public Image characterImage;
    public Image backgroundImage;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Plus d'une instance de UIManager dans la scène");
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        HideChoices();
    }

    public void DisplayPossibleChoices()
    {
        foreach (var obj in choiceBtn) obj.SetActive(true);
    }

    public void HideChoices()
    {
        foreach (var obj in choiceBtn) obj.SetActive(false);
    }
}