using Zenject;

namespace Tutorial
{
    public class TutorialStepsFactory
    {
        private readonly DiContainer _container;

        public TutorialStepsFactory(DiContainer container)
        {
            _container = container;
        }

        public T Create<T>() where T : TutorialStep
        {
            return _container.Resolve<T>();
        }
    }
}