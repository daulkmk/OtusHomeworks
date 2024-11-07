using Lessons.Game.Events;
using Lessons.Game.Turn.Visual;
using Lessons.Game.Turn.Visual.Tasks;

namespace Lessons.Game.Handlers.Visual
{
    public sealed class HealVisualHandler : BaseHandler<HealEvent>
    {
        private readonly VisualPipeline _visualPipeline;

        public HealVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
        }

        protected override void HandleEvent(HealEvent evt)
        {            
            _visualPipeline.AddTask(new HealHeroVisualTask(evt.Entity));
        }
    }
}