using UnityEngine;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private Step startStep;
        private Step _currentStep;

        private int _trust;
        
        private void Start()
        {
            _currentStep = startStep;
            DisplayStep();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                switch (_currentStep)
                {
                    case StepDiscours discours: BehaviourDiscours(discours); 
                        break;
                    case StepInformation info: BehaviourInfo(info);        
                        break;
                    case StepChoices choice:   BehaviourChoice(choice);  
                        break;
                }
            }
        }

        private void BehaviourDiscours(StepDiscours step)
        {
            _currentStep = step.nextStep;
            DisplayStep();
        }

        private void BehaviourInfo(StepInformation stepInformation)
        {
            if (_trust >= stepInformation.trustCost)
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
            _currentStep = stepChoices.stepChoice1.nextStep;
            StepChoice choice = stepChoices.stepChoice1;
            DisplayStep();
        }
        
        private void DisplayStep()
        {
            Debug.Log(_currentStep.message);
        }
    }
}