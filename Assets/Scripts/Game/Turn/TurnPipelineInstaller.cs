using System;
using Lessons.Common;
using Lessons.Game.Turn.Logic.Tasks;
using Lessons.Game.Turn.Visual.Tasks;
using VContainer;
using VContainer.Unity;

namespace Lessons.Game.Turn
{
    public sealed class TurnPipelineInstaller : IInitializable, IDisposable
    {
        private readonly TurnPipeline _turnPipeline;
        private readonly IObjectResolver _objectResolver;
        
        [Inject]
        public TurnPipelineInstaller(TurnPipeline turnPipeline, IObjectResolver objectResolver)
        {
            _turnPipeline = turnPipeline;
            _objectResolver = objectResolver;
        }

        void IInitializable.Initialize()
        {
            _turnPipeline.AddTask(_objectResolver.CreateInstance<StartTurnTask>());
            _turnPipeline.AddTask(_objectResolver.CreateInstance<PlayerTurnTask>());
            _turnPipeline.AddTask(_objectResolver.CreateInstance<StartVisualPipelineTask>());
            _turnPipeline.AddTask(_objectResolver.CreateInstance<FinishTurnTask>());
        }

        void IDisposable.Dispose()
        {
            _turnPipeline.ClearTasks();
        }
    }
}