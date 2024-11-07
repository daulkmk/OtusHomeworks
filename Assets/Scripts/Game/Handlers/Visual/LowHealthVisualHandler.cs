using Lessons.Game.Events;
using Lessons.Game.Turn.Visual;
using Lessons.Game.Turn.Visual.Tasks;

namespace Lessons.Game.Handlers.Visual
{
    public sealed class LowHealthVisualHandler : BaseHandler<LowHealthEvent>
    {
        private readonly VisualPipeline _visualPipeline;
        private readonly AudioPlayer _audioPlayer;
        
        public LowHealthVisualHandler(EventBus eventBus, VisualPipeline visualPipeline, AudioPlayer audioPlayer) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
            _audioPlayer = audioPlayer;
        }

        protected override void HandleEvent(LowHealthEvent evt)
        {
            _visualPipeline.AddTask(new LowHealthVisualTask(evt.Entity, _audioPlayer));
        }
    }
}