using Cysharp.Threading.Tasks;
using VContainer;

namespace Lessons.Game.Turn.Visual.Tasks
{
    public sealed class StartVisualPipelineTask : Task
    {
        private readonly VisualPipeline _visualPipeline;

        [Inject]
        public StartVisualPipelineTask(VisualPipeline visualPipeline)
        {
            _visualPipeline = visualPipeline;
        }

        protected override UniTask OnRun()
        {
            return _visualPipeline.Run();
        }

        protected override UniTask OnFinish()
        {
            _visualPipeline.ClearTasks();
            return UniTask.CompletedTask;
        }
    }
}