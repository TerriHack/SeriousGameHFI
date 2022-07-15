using UnityEngine;
using System;
using System.Collections;
using UI;
using UnityEngine.EventSystems;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private Step startStep;
        private Step _currentStep;


        private int _obtainedInfoIndex;
        private int _clickedIndex;
        
        [SerializeField] private string[] charactersNames;

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
            Invoke(nameof(DisplayStepMessage), 2.5f);
            CheckTargetMessage();
            DisplayName(2);
        }
        
        public void OnTapeScreen() 
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

            if (_currentStep.nextStep is StepChoices) _currentStep.nextStep.message = _currentStep.message;
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
            if (_currentStep.newBackground != null) uiManager.backgroundImage.sprite = _currentStep.newBackground;
            
            if (_currentStep.newMood != 0)
            {
                uiManager.characterImage.sprite = _currentStep.whoIsTalkingIndex switch
                {
                    0 => uiManager.rolandMoods[_currentStep.newMood],
                    2 => uiManager.rémiMoods[_currentStep.newMood],
                    _ => uiManager.characterImage.sprite
                };
            }
            
            switch (_currentStep)
            {
                case StepDiscours discours:
                    if (discours.isUnlockingTheTablet) uiManager.UnlockTablet();
                    DisplayStepMessage();
                    DisplayName(discours.whoIsTalkingIndex);
                    break;
                case StepInformation info:
                    if (uiManager.trust >= info.trustCost)
                    {
                        DisplayStepMessage();
                        GotNewInfo();
                        DisplayName(0);
                    }
                    else
                    {
                        _currentStep = info.nextStep;
                        DisplayCheck();
                    }
                    break;
                case StepChoices choices:
                    uiManager.dialogueText.text = _currentStep.message;
                    DisplayChoices(choices);
                    DisplayName(choices.whoIsTalkingIndex);
                    break;
            }
            CheckTargetMessage();
        }
        
        private void DisplayName(int whoIsTalkingIndex) => uiManager.nameText.text = charactersNames[whoIsTalkingIndex];
        
        private void DisplayStepMessage()
        {
            _currentDialogueSpeed = normalDialogueSpeed;
            StartCoroutine(TypeSentence(_currentStep.message));
        }

        private IEnumerator TypeSentence(string currentSentence)
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

            if (uiManager.dialogueText.text == currentSentence && _currentStep.nextStep is StepChoices) GoToNextScene();
        }

        private void SetTextById(params int[] ids)
        {
            foreach (var id in ids) uiManager.choiceText[id].text = StepChoiceByIndex(id).choiceText;
            uiManager.ChoiceOn = true;
        }
        
        private void DisplayChoices(StepChoices choices) => SetTextById(0,1,2);
       
        private StepChoice StepChoiceByIndex(int id)
        {
            if (_currentStep is not StepChoices step) throw new Exception("Current step isn't choice but you clicked on a choice ??");

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
            eventSystem.SetSelectedGameObject(null);
        
            _clickedIndex = index;
            
            if (StepChoiceByIndex(index).newMood != 0)
            {
                uiManager.characterImage.sprite = _currentStep.whoIsTalkingIndex switch
                {
                    0 => uiManager.rolandMoods[StepChoiceByIndex(index).newMood],
                    2 => uiManager.rémiMoods[StepChoiceByIndex(index).newMood],
                    _ => uiManager.characterImage.sprite
                };
            }

            if (StepChoiceByIndex(index).isGoodAnswer) uiManager.trust += 10;
            else if (StepChoiceByIndex(index).isBadAnswer) uiManager.trust -= 10;
            
            uiManager.UpdateTrustText();
            
            StartCoroutine(TypeSentence(StepChoiceByIndex(index).clientResponse));
            uiManager.ChoiceOn = false; 
            CheckTargetMessage();
        }

        private void GotNewInfo()
        {
            _obtainedInfoIndex++;
            uiManager.infoImages[_obtainedInfoIndex -= 1].sprite = uiManager.gotInfoSprites[_obtainedInfoIndex -= 1];
        }
    }
}