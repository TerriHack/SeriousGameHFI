using UnityEngine;
using System;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private Step startStep;
        private Step _currentStep;

        public int trust;

        private int clickedIndex;

        private UIManager uiManager => UIManager.instance;
        
        private void Start()
        {
            _currentStep = startStep;
            DisplayStep();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Pour passer au discour d'après
            {
                if (_currentStep is StepChoices choices)
                {
                    _currentStep = clickedIndex switch
                    {
                        0 => choices.stepChoice1.nextStepAnswer != null ? _currentStep = choices.stepChoice1.nextStepAnswer : _currentStep.nextStep,
                        1 => choices.stepChoice2.nextStepAnswer != null ? _currentStep = choices.stepChoice2.nextStepAnswer : _currentStep.nextStep,
                        2 => choices.stepChoice3.nextStepAnswer != null ? _currentStep = choices.stepChoice3.nextStepAnswer : _currentStep.nextStep,
                        _ => _currentStep
                    };

                    clickedIndex =-1;
                    DisplayCheck();
                    return;
                }
                
                _currentStep = _currentStep.nextStep;
                DisplayCheck();
            }
        }

        private void DisplayCheck()
        {
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

        private void DisplayStep() =>  uiManager.dialogueText.text = _currentStep.message;

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
            clickedIndex = index;
            
            if (StepChoiceByIndex(index).isGoodAnswer)
            {
                trust++;
            }

            uiManager.dialogueText.text = StepChoiceByIndex(index).clientResponse;
            uiManager.HideChoices(); 
        }
    }
}