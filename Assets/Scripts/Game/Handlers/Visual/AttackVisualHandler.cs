using Lessons.Game.Events;
using Lessons.Game.Turn.Visual;
using Lessons.Game.Turn.Visual.Tasks;
using Lessons.Level;

namespace Lessons.Game.Handlers.Visual
{
    public sealed class AttackVisualHandler : BaseHandler<AttackEvent>
    {
        private readonly VisualPipeline _visualPipeline;
        private readonly HeroesViewMap _heroesViewMap;

        public AttackVisualHandler(EventBus eventBus, VisualPipeline visualPipeline, HeroesViewMap heroesViewMap) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
            _heroesViewMap = heroesViewMap;
        }

        protected override void HandleEvent(AttackEvent evt)
        {
            var attacker = _heroesViewMap.GetViewByHero(evt.Entity);
            var target = _heroesViewMap.GetViewByHero(evt.Target);
            
            _visualPipeline.AddTask(new AttackVisualTask(attacker, target));
        }
    }
}