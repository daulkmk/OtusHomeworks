using UI;

namespace Lessons.Entities.Common.Components
{
    public sealed class HeroViewComponent
    {
        private readonly HeroView _view;

        public HeroViewComponent(HeroView hero)
        {
            _view = hero;
        }

        public void SetActive(bool active) => _view.SetActive(active);

        public void SetStats(int health, int strength) 
        {
            _view.SetStats($"{health} / {strength}");
        }
    }
}