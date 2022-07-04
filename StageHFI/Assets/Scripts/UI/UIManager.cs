using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject[] choiceBtn;
    public TextMeshProUGUI[] choiceText;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    public Image characterImage;
    public Image backgroundImage;

    public Sprite[] rolandMoods;
    public Sprite[] rémiMoods;

    public TextMeshProUGUI trustPercentText;

    public Sprite blockedInfoSprite;
    public Sprite gotInfoSprite;

    public Image[] infoIcones;

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