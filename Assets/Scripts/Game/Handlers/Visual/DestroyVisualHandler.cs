using Lessons.Game.Events;
using Lessons.Game.Turn.Visual;
using Lessons.Game.Turn.Visual.Tasks;

namespace Lessons.Game.Handlers.Visual
{
    public sealed class DestroyVisualHandler : BaseHandler<DestroyEvent>
    {
        private readonly VisualPipeline _visualPipeline;
        
        public DestroyVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
        }

        protected override void HandleEvent(DestroyEvent evt)
        {
            _visualPipeline.AddTask(new DestroyVisualTask(evt.Entity));
        }
    }
}