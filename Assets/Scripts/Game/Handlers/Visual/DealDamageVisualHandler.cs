using Lessons.Game.Events;
using Lessons.Game.Turn.Visual;
using Lessons.Game.Turn.Visual.Tasks;

namespace Lessons.Game.Handlers.Visual
{
    public sealed class DealDamageVisualHandler : BaseHandler<DealDamageEvent>
    {
        private readonly VisualPipeline _visualPipeline;
        
        public DealDamageVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
        }

        protected override void HandleEvent(DealDamageEvent evt)
        {
            _visualPipeline.AddTask(new DealDamageVisualTask(evt.Entity));
        }
    }
}