using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
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

        private bool _choiceOn;

        public bool ChoiceOn
        {
            private get
            {
                return _choiceOn;
            }
            set
            {
                _choiceOn = value;
                if (_choiceOn)
                {
                    StartCoroutine(DisplayPossibleChoices());
                    StartCoroutine(DisplayTextChoices());
                }
                else 
                {
                    StartCoroutine(HideChoices());
                    StartCoroutine(HideTextChoices());
                }
            }
        }
    

        [SerializeField] [Range(0,1)] private float fadeButtonTime;
        [SerializeField] [Range(0,1)] private float delayBetweenButton;

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
            foreach (var obj in choiceBtn) LeanTween.alpha(obj.GetComponent<RectTransform>(), 0, 0);
            foreach (var txt in choiceText) txt.alpha = 0;
        }
        
        private IEnumerator DisplayPossibleChoices()
        {
            foreach (var obj in choiceBtn)
            {
                LeanTween.alpha(obj.GetComponent<RectTransform>(), 1f, fadeButtonTime);
                yield return new WaitForSeconds(delayBetweenButton);
            }
        }
        private IEnumerator DisplayTextChoices()
        {
            foreach (var txt in choiceText)
            {
                LeanTween.alpha(txt.GetComponent<RectTransform>(), 1f, fadeButtonTime);
                yield return new WaitForSeconds(delayBetweenButton);
            }
        }
        private IEnumerator HideChoices()
        {
            foreach (var obj in choiceBtn)
            {
                LeanTween.alpha(obj.GetComponent<RectTransform>(), 0f, fadeButtonTime);
                yield return new WaitForSeconds(delayBetweenButton);
            }
        }
        private IEnumerator HideTextChoices()
        {
            foreach (var txt in choiceText)
            {
                LeanTween.alpha(txt.GetComponent<RectTransform>(), 0f, fadeButtonTime);
                yield return new WaitForSeconds(delayBetweenButton);
            }
        }
    }
}