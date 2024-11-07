using Lessons.Entities;
using Lessons.Game;
using Lessons.Game.Handlers.Effects;
using Lessons.Game.Handlers.Logic;
using Lessons.Game.Handlers.Visual;
using Lessons.Game.Services;
using Lessons.Game.Turn;
using Lessons.Game.Turn.Visual;
using Lessons.Level;
using UI;
using VContainer;
using VContainer.Unity;

namespace Lessons.Utils
{
    public sealed class SceneScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            ConfigureLevel(builder);
            ConfigurePlayer(builder);
            ConfigureHandlers(builder);
            ConfigureTurn(builder);

            //builder.RegisterComponentInHierarchy<EntityInstaller>();
            builder.RegisterComponentInHierarchy<UIService>();
            builder.RegisterComponentInHierarchy<AudioPlayer>();
        }

        private void ConfigureLevel(IContainerBuilder builder)
        {
            // builder.RegisterComponentInHierarchy<TileMap>();
            // builder.Register<EntityMap>(Lifetime.Singleton);
            // builder.Register<LevelMap>(Lifetime.Singleton);

            builder.Register<HeroesViewMap>(Lifetime.Singleton);
        }

        private void ConfigurePlayer(IContainerBuilder builder)
        {
            //builder.RegisterComponentInHierarchy<KeyboardInput>();
            builder.Register<PlayersService>(Lifetime.Singleton);
        }

        private void ConfigureHandlers(IContainerBuilder builder)
        {
            builder.Register<EventBus>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<SelectHeroHandler>();
            builder.RegisterEntryPoint<TurnStartedHandler>();
            builder.RegisterEntryPoint<PlayerSelectedAttackTargetEventHandler>();

            // builder.RegisterEntryPoint<ApplyDirectionHandler>();
            // builder.RegisterEntryPoint<CollideHandler>();

            builder.RegisterEntryPoint<AttackHandler>();
            builder.RegisterEntryPoint<DealDamageHandler>();
            builder.RegisterEntryPoint<HealHandler>();
            builder.RegisterEntryPoint<UseShieldHandler>();
            builder.RegisterEntryPoint<DestroyHandler>();
            
            // builder.RegisterEntryPoint<MoveHandler>();
            // builder.RegisterEntryPoint<ForceDirectionHandler>();
            
            builder.RegisterEntryPoint<SelectDefaultTargetEffectHandler>();
            builder.RegisterEntryPoint<SelectRandomTargetWitchChanceEffectHandler>();

            builder.RegisterEntryPoint<ApplyDamageEffectHandler>();
            builder.RegisterEntryPoint<ApplyDamageToAllOtherHeroesEffectHandler>();
            builder.RegisterEntryPoint<ApplyDamageWithShieldEffectHandler>();

            builder.RegisterEntryPoint<DealDamageEffectHandler>();
            builder.RegisterEntryPoint<DealDamageBackwardsEffectHandler>();
            builder.RegisterEntryPoint<DealDamageToRandomEnemyEffectHandler>();
            builder.RegisterEntryPoint<DealDamageWithRandomTargetEffectHandler>();
            builder.RegisterEntryPoint<VampDamageEffectHandler>();
            builder.RegisterEntryPoint<HealRandomAllyEffectHandler>();
            builder.RegisterEntryPoint<SkipTutnEffectHandler>();
            builder.RegisterEntryPoint<AbilityUsedEffectHandler>();
            // builder.RegisterEntryPoint<PushEffectHandler>();
        }
        
        private void ConfigureTurn(IContainerBuilder builder)
        {
            builder.Register<TurnPipeline>(Lifetime.Singleton);
            builder.RegisterEntryPoint<TurnPipelineInstaller>();
            builder.RegisterEntryPoint<TurnPipelineRunner>();

            builder.Register<VisualPipeline>(Lifetime.Singleton);
            // builder.RegisterEntryPoint<MoveVisualHandler>();
            builder.RegisterEntryPoint<DestroyVisualHandler>();
            builder.RegisterEntryPoint<HealVisualHandler>();
            builder.RegisterEntryPoint<AttackVisualHandler>();
            builder.RegisterEntryPoint<DealDamageVisualHandler>();
            builder.RegisterEntryPoint<LowHealthVisualHandler>();
            builder.RegisterEntryPoint<AbilityUsedVisualHandler>();
        }
    }
}