using Entities;
using Lessons.Entities.Common.Components;
using UnityEngine;

namespace Lessons.Entities.Hero
{
    [RequireComponent(typeof(HeroModel))]
    [DefaultExecutionOrder(-100)]
    public class HeroEntity : MonoEntityBase
    {
        private void Awake()
        {
            HeroModel model = GetComponent<HeroModel>();
            Add(new HitPointsComponent(model.life.hitPoints, model.life.maxHitPoints));
            Add(new DeathComponent(model.life.isDead));
            Add(new DestroyComponent(gameObject));
            Add(new TransformComponent(model.position.transform));
            Add(new StatsComponent(model.stats));
            Add(new HeroViewComponent(model.view));
            Add(new WeaponComponent(model.weapon));
            Add(new ShieldComponent(model.shield));
            Add(new DamageHandlerComponent(model.damageHandler));
            Add(new TargetSelectorComponent(model.targetSelector));
            Add(new SFXComponent(model.sfx));
        }

        private void Start() => UpdateStatsView();

        public void UpdateStatsView()
        {
            var viewComponent = Get<HeroViewComponent>();
            var health = Get<HitPointsComponent>();
            var stats = Get<StatsComponent>();

            viewComponent.SetStats(health.Value, stats.Strength);
        }
    }
}