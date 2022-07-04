using UnityEngine;
using System;
using System.Collections;
using UnityEngine.EventSystems;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private Step startStep;
        private Step _currentStep;

        private int _trust;

        private int _obtainedInfoIndex;

        private int _clickedIndex;

        private UIManager uiManager => UIManager.instance;
        
        [SerializeField] [Range(0, 0.2f)] private float normalDialogueSpeed;
        [SerializeField] [Range(0, 0.2f)] private float accelerateDialogueSpeed;
        private float _currentDialogueSpeed;

        private string _targetMessage;

        [SerializeField] private EventSystem eventSystem;
        
        private void Start()
        {
            _currentDialogueSpeed = normalDialogueSpeed;
            _currentStep = startStep;
            _clickedIndex = -1;
            _trust = 50;
            uiManager.trustPercentText.text = _trust + "%";
            foreach (var image in uiManager.infoIcones)
            {
                image.sprite = uiManager.blockedInfoSprite;
            }
            DisplayStep();
            CheckTargetMessage();
        }

        private void Update()
        {
            // Pour passer au discour d'après
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (uiManager.dialogueText.text != _targetMessage)
                {
                    _currentDialogueSpeed = accelerateDialogueSpeed;
                    return;
                }
                
                if (_currentStep is StepChoices)
                {
                    if (_clickedIndex >= 0) GoToNextScene();
                }
                else GoToNextScene();
            }
        }

        private void GoToNextScene()
        {
            if (_currentStep is StepChoices choices)
            {
                _currentStep = _clickedIndex switch
                {
                    0 => choices.stepChoice1.nextStepAnswer != null ? _currentStep = choices.stepChoice1.nextStepAnswer : _currentStep.nextStep, 
                    1 => choices.stepChoice2.nextStepAnswer != null ? _currentStep = choices.stepChoice2.nextStepAnswer : _currentStep.nextStep,
                    2 => choices.stepChoice3.nextStepAnswer != null ? _currentStep = choices.stepChoice3.nextStepAnswer : _currentStep.nextStep,
                    _ => _currentStep
                };
                
                _clickedIndex = -1;
                DisplayCheck();
                return;
            }
                
            _currentStep = _currentStep.nextStep; 
            DisplayCheck();
        }

        private void CheckTargetMessage()
        {
            if (_currentStep is not StepChoices stepChoices) _targetMessage = _currentStep.message;
            else
            {
                _targetMessage = _clickedIndex switch
                {
                    0 => stepChoices.stepChoice1.clientResponse,
                    1 => stepChoices.stepChoice2.clientResponse,
                    2 => stepChoices.stepChoice3.clientResponse,
                    -1 => _currentStep.message,
                    _ => _currentStep.message
                };
            }
        }

        private void DisplayCheck()
        {
            if (_currentStep.newMood != null) uiManager.characterImage.sprite = _currentStep.newMood;
            if (_currentStep.newBackground != null) uiManager.backgroundImage.sprite = _currentStep.newBackground;

            switch (_currentStep)
            {
                case StepDiscours: 
                    DisplayStep();
                    break;
                case StepInformation info:
                    if (_trust >= info.trustCost)
                    {
                        DisplayStep();
                        GotNewInfo();
                    }
                    else
                    {
                        _currentStep = info.nextStep;
                        DisplayCheck();
                    }
                    break;
                case StepChoices choices: 
                    DisplayStep();
                    DisplayChoices(choices);
                    break;
            }
            CheckTargetMessage();
        }

        private void DisplayStep()
        {
            _currentDialogueSpeed = normalDialogueSpeed;
            StartCoroutine(TypeSentence(_currentStep.message));
        }

        IEnumerator TypeSentence(string currentSentence)
        {
            uiManager.dialogueText.text = "";
            bool inBalise = false;

            foreach (char letter in currentSentence)
            {
                uiManager.dialogueText.text += letter;
            
                if (inBalise && letter != '>') continue;

                if (inBalise && letter == '>')
                {
                    inBalise = false;
                    continue;
                }

                if (letter == '<')
                {
                    inBalise = true;
                    continue;
                }
            
                yield return new WaitForSeconds(_currentDialogueSpeed);
            }
        }

        private void SetTextById(params int[] ids)
        {
            foreach (var id in ids)
            {
                uiManager.choiceText[id].text = StepChoiceByIndex(id).choiceText;
            }
            
            uiManager.DisplayPossibleChoices();
        }
        
        private void DisplayChoices(StepChoices choices) => SetTextById(0,1,2);
       
        private StepChoice StepChoiceByIndex(int id)
        {
            if (_currentStep is not StepChoices step)
            {
                throw new Exception("Current step isn't choice but you clicked on a choice ??");
            }

            return id switch
            {
                0 => step.stepChoice1,
                1 => step.stepChoice2,
                2 => step.stepChoice3,
                _ => throw new Exception("Did you forget to set an index on your button? all the same those GDs")
            };
        }
        
        public void ClickChoice(int index)
        {
            _clickedIndex = index;
            
            if (StepChoiceByIndex(index).isGoodAnswer) _trust += 10;
            else _trust -= 10;

            uiManager.trustPercentText.text = _trust + "%";
            
            StartCoroutine(TypeSentence(StepChoiceByIndex(index).clientResponse));
            uiManager.HideChoices(); 
            CheckTargetMessage();
            eventSystem.SetSelectedGameObject(null);
        }

        private void GotNewInfo()
        {
            _obtainedInfoIndex++;
            uiManager.infoIcones[_obtainedInfoIndex -= 1].sprite = uiManager.gotInfoSprite;
        }
    }
}
