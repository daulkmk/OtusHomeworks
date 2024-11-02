using JetBrains.Annotations;
using Lessons.Game.Events;
using Lessons.Game.Turn.Visual;
using Lessons.Game.Turn.Visual.Tasks;
using Lessons.Level;

namespace Lessons.Game.Handlers.Visual
{
    [UsedImplicitly]
    public sealed class MoveVisualHandler : BaseHandler<MoveEvent>
    {
        private readonly VisualPipeline _visualPipeline;
        private readonly LevelMap _levelMap;
        
        public MoveVisualHandler(EventBus eventBus, VisualPipeline visualPipeline, LevelMap levelMap) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
            _levelMap = levelMap;
        }

        protected override void HandleEvent(MoveEvent evt)
        {
            _visualPipeline.AddTask(new MoveVisualTask(evt.Entity, 
                _levelMap.Tiles.CoordinatesToPosition(evt.Coordinates)));
        }
    }
}