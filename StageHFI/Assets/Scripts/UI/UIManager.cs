using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject[] choiceBtn;
    public TextMeshProUGUI[] choiceText;

    public TextMeshProUGUI dialogueText;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Plus d'une instance de UIManager dans la scène");
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