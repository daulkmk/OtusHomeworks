using Lessons.Game.Events;
using Lessons.Game.Turn.Visual;
using Lessons.Game.Turn.Visual.Tasks;

namespace Lessons.Game.Handlers.Visual
{
    public sealed class AbilityUsedVisualHandler : BaseHandler<AbilityUsedEvent>
    {
        private readonly VisualPipeline _visualPipeline;
        private readonly AudioPlayer _audioPlayer;

        public AbilityUsedVisualHandler(EventBus eventBus, VisualPipeline visualPipeline, AudioPlayer audioPlayer) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
            _audioPlayer = audioPlayer;
        }

        protected override void HandleEvent(AbilityUsedEvent evt)
        {
            _visualPipeline.AddTask(new AbilityUsedVisualTask(evt.Entity, _audioPlayer));
        }
    }
}