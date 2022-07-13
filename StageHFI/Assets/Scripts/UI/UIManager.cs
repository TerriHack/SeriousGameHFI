using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        [Header("Choice")]
        public GameObject[] choiceBtn;
        public TextMeshProUGUI[] choiceText;
        private bool _choiceOn;

        [Header("Dialogue")]
        [SerializeField] private RectTransform discourPanel;
        public TextMeshProUGUI dialogueText;
        public TextMeshProUGUI nameText;

        public Image characterImage;
        public Image backgroundImage;

        [SerializeField] private RectTransform tablet;

        [Header("Moods")]
        public Sprite[] rolandMoods;
        public Sprite[] rémiMoods;

        [Header("Trust")]
        [HideInInspector] public int trust;
        public TextMeshProUGUI trustText;
        [SerializeField] private Image trustBanerImage;
        [SerializeField] private Gradient trustBanerGradient;

        [Header("Info Icones")]
        public Sprite blockedInfoSprite;
        public Sprite[] gotInfoSprites;
        public Image[] infoImages;


       public bool ChoiceOn
        {
            private get
            {
                return _choiceOn;
            }
            set
            {
                _choiceOn = value;
                StartCoroutine(_choiceOn ? DisplayPossibleChoices() : HideChoices(btnFadeDelay, delayBetweenButton));
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
            UpdateTrustText();
            
            trust = 50;
            foreach (var image in infoImages) image.sprite = blockedInfoSprite;

            tablet.gameObject.SetActive(false);

            StartCoroutine(HideChoices(0,0));
            
            LeanTween.alpha(characterImage.GetComponent<RectTransform>(), 1, 1.5f).setDelay(0.5f);
            LeanTween.alpha(discourPanel, 1, 1.5f).setDelay(1.5f);
        }

        private void Update()
        {
            if (nameText.alpha < 1) nameText.alpha += Time.deltaTime * 0.2f;
        } 
        
        private void FixedUpdate()
        {
            if (_choiceOn) foreach (var txt in choiceText) DisplayTextChoices(txt);
            else foreach (var txt in choiceText) HideTextChoices(txt);
        }

        public void UpdateTrustText() => trustText.text = "confiance en vous : " + trust + "%";
        public void UpdateTrustBanerColor() => trustBanerImage.color = trustBanerGradient.Evaluate(trust / 100f);
        

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
            if (txt.transform.parent.gameObject.activeSelf == false) return;
            if(txt.alpha < 1 && txt.GetComponentInParent<Image>().color.a >= 1) txt.alpha += Time.fixedDeltaTime * txtFadeSpeed;
        } 
        
        private IEnumerator HideChoices(float fadeDelay, float delayBetweenObj)
        {
            foreach (var obj in choiceBtn)
            {
                LeanTween.alpha(obj.GetComponent<RectTransform>(), 0f, fadeDelay);
                StartCoroutine(DisableChoices(obj, fadeDelay));
                yield return new WaitForSeconds(delayBetweenObj);
            }
        }

        private void HideTextChoices(TMP_Text txt)
        {
            if (txt.transform.parent.gameObject.activeSelf == false || txt.GetComponentInParent<Image>().color.a <= 0) txt.alpha = 0;
            else if (txt.alpha > 0) txt.alpha -= Time.deltaTime * txtFadeSpeed;
        }
        
        private IEnumerator DisableChoices(GameObject go, float fadeDelay)
        {
            yield return new WaitForSeconds(fadeDelay + 0.1f);
            go.SetActive(false);
        }

        public void UnlockTablet()
        {
            tablet.gameObject.SetActive(true);
            LeanTween.alpha(tablet.GetComponent<RectTransform>(), 1, 1f).setDelay(1);
        }
    }
}