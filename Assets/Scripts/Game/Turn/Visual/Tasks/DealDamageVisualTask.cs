using Cysharp.Threading.Tasks;
using Entities;
using Lessons.Entities.Common.Components;

namespace Lessons.Game.Turn.Visual.Tasks
{
    public sealed class DealDamageVisualTask : Task
    {
        private readonly HeroViewComponent _heroView;
        private readonly HitPointsComponent _hitPointsComponent;
        private readonly StatsComponent _statsComponent;

        public DealDamageVisualTask(IEntity entity)
        {
            _heroView = entity.Get<HeroViewComponent>();
            _hitPointsComponent = entity.Get<HitPointsComponent>();
            _statsComponent = entity.Get<StatsComponent>();
        }

        protected override UniTask OnRun()
        {
            _heroView.SetStats(_hitPointsComponent.Value, _statsComponent.Strength);
            return UniTask.CompletedTask;
        }
    }
}