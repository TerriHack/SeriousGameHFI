using UnityEngine;
using System;
using System.Collections;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private Step startStep;
        private Step _currentStep;

        public int trust;

        [SerializeField] private int _clickedIndex = -1;

        private UIManager uiManager => UIManager.instance;
        
        [SerializeField] [Range(0, 0.2f)] private float normalDialogueSpeed;
        [SerializeField] [Range(0, 0.2f)] private float accelerateDialogueSpeed;
        private float _currentDialogueSpeed;
        
        private void Start()
        {
            _currentDialogueSpeed = normalDialogueSpeed;
            _currentStep = startStep;
            DisplayStep();
        }

        private void Update()
        {
            Debug.Log(_currentStep);

            if (Input.GetKeyDown(KeyCode.Space)) // Pour passer au discour d'après
            {
                if (uiManager.dialogueText.text != _currentStep.message)
                {
                    _currentDialogueSpeed = accelerateDialogueSpeed;
                    if (_currentStep is not StepChoices) return;
                }
                
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
                    if (trust >= info.trustCost) DisplayStep();
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
            
            if (StepChoiceByIndex(index).isGoodAnswer) trust += 10;

            StartCoroutine(TypeSentence(StepChoiceByIndex(index).clientResponse));
            uiManager.HideChoices(); 
        }
    }
}
