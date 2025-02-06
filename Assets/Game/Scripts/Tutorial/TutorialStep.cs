using System;
using Lessons.Lesson_Components.UI;

namespace Tutorial
{
    public abstract class TutorialStep
    {
        protected readonly TutorialScreen _tutorialScreen;

        public event Action<TutorialStep> OnComplete;
        public event Action<TutorialStep> OnStart;

        public TutorialStep(TutorialScreen tutorialScreen)
        {
            _tutorialScreen = tutorialScreen;
        }

        public abstract Step Step { get; }
        public abstract void Start();
        public abstract void End();

        protected void FireStartEvent() => OnStart?.Invoke(this);
        protected void FireCompleteEvent() => OnComplete?.Invoke(this);
    }
}