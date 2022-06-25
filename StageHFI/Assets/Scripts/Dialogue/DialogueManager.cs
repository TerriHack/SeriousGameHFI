using UnityEngine;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private Step startStep;
        private Step _currentStep;

        public int trust;
        
        private void Start()
        {
            _currentStep = startStep;
            DisplayStep();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Pour passer au discour d'après
            {
                _currentStep = _currentStep.nextStep;
                DisplayCheck();
            }
        }

        private void DisplayCheck()
        {
            switch (_currentStep)
            {
                case StepDiscours :
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
                    DisplayChoices();
                    break;
            }
        }

        private void BehaviourDiscours(StepDiscours step)
        {
            if(step.nextStep is StepInformation info)
            {
               
            }
            else
            {
                _currentStep = step.nextStep;
                DisplayStep();
            }
        }

        private void BehaviourInfo(StepInformation stepInformation)
        {
            if (trust >= stepInformation.trustCost)
            {
                _currentStep = stepInformation;
            }
            else
            {
                _currentStep = stepInformation.nextStep;
            }
            DisplayStep();
        }

        private void BehaviourChoice(StepChoices stepChoices)
        {
           // _currentStep = stepChoices.stepChoice1.nextStep;
            StepChoice choice = stepChoices.stepChoice1;
            DisplayStep();
        }
        
        private void DisplayStep()
        {
            Debug.Log(_currentStep.message);
        }

        private void DisplayChoices()
        {
            
        }
    }
}