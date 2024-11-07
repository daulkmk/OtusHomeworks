using Entities;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;

namespace Lessons.Game.Handlers.Logic
{
    public sealed class SelectHeroHandler : BaseHandler<SelectHeroEvent>
    {
        public SelectHeroHandler(EventBus eventBus) : base(eventBus)
        {
        }

        protected override void HandleEvent(SelectHeroEvent evt)
        {
            if (evt.Previous != null)
                SetActive(evt.Previous, false);
            
            if (evt.Selected != null)
                SetActive(evt.Selected, true);
        }

        private void SetActive(IEntity hero, bool active)
        {
            var viewComponent = hero.Get<HeroViewComponent>();
            viewComponent.SetActive(active);
        }
    }
}