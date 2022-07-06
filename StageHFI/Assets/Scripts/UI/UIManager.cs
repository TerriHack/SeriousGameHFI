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

        [SerializeField] private RectTransform tablet;
        [SerializeField] private RectTransform discourPanel;

        public Sprite[] rolandMoods;
        public Sprite[] rémiMoods;

        public TextMeshProUGUI trustPercentText;

        public Sprite blockedInfoSprite;
        public Sprite gotInfoSprite;

        public Image[] infoIcones;

       [SerializeField] private bool _choiceOn;

       public bool ChoiceOn
        {
            private get
            {
                return _choiceOn;
            }
            set
            {
                _choiceOn = value;
                StartCoroutine(_choiceOn ? DisplayPossibleChoices() : HideChoices());
            }
        }

        [SerializeField] [Range(0,5)] private float btnFadeDelay;
        [SerializeField] [Range(0,5)] private float delayBetweenButton;
        [SerializeField] [Range(0,5)] private float txtFadeSpeed;

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
            tablet.gameObject.SetActive(false);

            foreach (var obj in choiceBtn)
            {
                LeanTween.alpha(obj.GetComponent<RectTransform>(), 0, 0);
                obj.SetActive(false);
            }
            
            LeanTween.alpha(characterImage.GetComponent<RectTransform>(), 1, 1.5f).setDelay(0.5f);
            LeanTween.alpha(discourPanel, 0.7f, 1.5f).setDelay(1.5f);
        }

        private void FixedUpdate()
        {
            if (_choiceOn) foreach (var txt in choiceText) DisplayTextChoices(txt);
            else if (choiceBtn[1].activeSelf) foreach (var txt in choiceText) HideTextChoices(txt);
        }

        private IEnumerator DisplayPossibleChoices()
        {
            foreach (var obj in choiceBtn)
            {
                obj.SetActive(true);
                LeanTween.alpha(obj.GetComponent<RectTransform>(), 1f, btnFadeDelay);
                yield return new WaitForSeconds(delayBetweenButton);
            }
        }

        private void DisplayTextChoices(TMP_Text txt)
        {
            if(txt.alpha < 1 && txt.GetComponentInParent<Image>().color.a >= 1) txt.alpha += Time.fixedDeltaTime * txtFadeSpeed;
        } 
        
        private IEnumerator HideChoices()
        {
            foreach (var obj in choiceBtn)
            {
                LeanTween.alpha(obj.GetComponent<RectTransform>(), 0f, btnFadeDelay);
                StartCoroutine(DisableChoices(obj));
                yield return new WaitForSeconds(delayBetweenButton);
            }
        }

        private void HideTextChoices(TMP_Text txt)
        {
            if (txt.GetComponentInParent<Image>().color.a <= 0) txt.alpha = 0;
            else if (txt.alpha > 0) txt.alpha -= Time.deltaTime * txtFadeSpeed;
        }
        
        private IEnumerator DisableChoices(GameObject go)
        {
            yield return new WaitForSeconds(btnFadeDelay + btnFadeDelay / 2);
            go.SetActive(false);
        }

        public void UnlockTablet()
        {
            tablet.gameObject.SetActive(true);
            LeanTween.alpha(tablet, 1, 0.5f).setDelay(1);
        }
    }
}