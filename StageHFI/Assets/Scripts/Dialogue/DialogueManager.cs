using UnityEngine;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private Step startStep;
        private Step _currentStep;

        public int trust;

        private bool clicked1;
        private bool clicked2;
        private bool clicked3;

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
                    if (clicked1)
                    {
                        _currentStep = choices.stepChoice1.nextStepAnswer != null ? _currentStep = choices.stepChoice1.nextStepAnswer : _currentStep.nextStep;
                    }
                    else if (clicked2)
                    {
                        _currentStep = choices.stepChoice2.nextStepAnswer != null ? _currentStep = choices.stepChoice2.nextStepAnswer : _currentStep.nextStep;
                    }
                    else if (clicked3)
                    {
                        _currentStep = choices.stepChoice3.nextStepAnswer != null ? _currentStep = choices.stepChoice3.nextStepAnswer : _currentStep.nextStep;
                    }

                    clicked1 = false;
                    clicked2 = false;
                    clicked3 = false;
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

        private void DisplayStep()
        {
            UIManager.instance.dialogueText.text = _currentStep.message;
        }

        private void DisplayChoices(StepChoices choices)
        {
            UIManager.instance.choiceText[0].text = choices.stepChoice1.choiceText;
            UIManager.instance.choiceText[1].text = choices.stepChoice2.choiceText;
            UIManager.instance.choiceText[2].text = choices.stepChoice3.choiceText;

            UIManager.instance.DisplayPossibleChoices();
        }

        public void ClickedChoice_1(StepChoices choices)
        {
            clicked1 = true;
            if (choices.stepChoice1.isGoodAnswer) trust++;

            UIManager.instance.dialogueText.text = choices.stepChoice1.clientResponse;
            UIManager.instance.HideChoices();
        }

        public void ClickedChoice_2(StepChoices choices)
        {
            clicked2 = true;
            if (choices.stepChoice2.isGoodAnswer) trust++;

            UIManager.instance.dialogueText.text = choices.stepChoice2.clientResponse;
            UIManager.instance.HideChoices();
        }

        public void ClickedChoice_3(StepChoices choices)
        {
            clicked3 = true;
            if (choices.stepChoice3.isGoodAnswer) trust++;

            UIManager.instance.dialogueText.text = choices.stepChoice3.clientResponse;
            UIManager.instance.HideChoices();
        }
    }
}