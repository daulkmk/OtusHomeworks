using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class TutorialManager
    {
        private readonly List<TutorialStep> _steps = new();

        private TutorialStep _currentStep = null;

        public TutorialManager(TutorialStepsFactory stepsFactory)
        {
            _steps.Add(stepsFactory.Create<HowToMoveAndShootStep>());
            _steps.Add(stepsFactory.Create<KillZombiesStep>());
            _steps.Add(stepsFactory.Create<StayAliveStep>());

            StartStep( _steps[0]);
        }

        private void StartStep(TutorialStep step)
        {
            _currentStep = step;
            _currentStep.OnComplete += OnStepComplete;
            _currentStep.Start();
        }

        private void EndStep(TutorialStep step)
        {
            step.End();
            step.OnComplete -= OnStepComplete;
        }

        private void OnStepComplete(TutorialStep completedStep)
        {
            EndStep(completedStep);

            int index = _steps.IndexOf(completedStep);
            if (index == _steps.Count - 1)
            {
                Debug.Log("Tutorial complete!");
                _currentStep = null;
            }
            else
            {
                StartStep(_steps[index + 1]);
            }
        }
    }
}