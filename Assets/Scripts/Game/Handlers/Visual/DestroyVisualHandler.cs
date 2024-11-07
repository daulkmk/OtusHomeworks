using Lessons.Game.Events;
using Lessons.Game.Turn.Visual;
using Lessons.Game.Turn.Visual.Tasks;

namespace Lessons.Game.Handlers.Visual
{
    public sealed class DestroyVisualHandler : BaseHandler<DestroyEvent>
    {
        private readonly VisualPipeline _visualPipeline;
        private readonly AudioPlayer _audioPlayer;

        public DestroyVisualHandler(EventBus eventBus, VisualPipeline visualPipeline, AudioPlayer audioPlayer) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
            _audioPlayer = audioPlayer;
        }

        protected override void HandleEvent(DestroyEvent evt)
        {
            _visualPipeline.AddTask(new DestroyVisualTask(evt.Entity, _audioPlayer));
        }
    }
}