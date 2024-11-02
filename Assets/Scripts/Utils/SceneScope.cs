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

            builder.RegisterComponentInHierarchy<EntityInstaller>();
            builder.RegisterComponentInHierarchy<UIService>();
        }

        private void ConfigureLevel(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<TileMap>();
            builder.Register<EntityMap>(Lifetime.Singleton);
            builder.Register<LevelMap>(Lifetime.Singleton);
        }

        private void ConfigurePlayer(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<KeyboardInput>();
            builder.RegisterComponentInHierarchy<PlayerService>();
        }

        private void ConfigureHandlers(IContainerBuilder builder)
        {
            builder.Register<EventBus>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<ApplyDirectionHandler>();
            builder.RegisterEntryPoint<CollideHandler>();
            builder.RegisterEntryPoint<AttackHandler>();
            builder.RegisterEntryPoint<DealDamageHandler>();
            builder.RegisterEntryPoint<DestroyHandler>();
            builder.RegisterEntryPoint<MoveHandler>();
            builder.RegisterEntryPoint<ForceDirectionHandler>();
            
            builder.RegisterEntryPoint<DealDamageEffectHandler>();
            builder.RegisterEntryPoint<PushEffectHandler>();
        }
        
        private void ConfigureTurn(IContainerBuilder builder)
        {
            builder.Register<TurnPipeline>(Lifetime.Singleton);
            builder.RegisterEntryPoint<TurnPipelineInstaller>();
            builder.RegisterComponentInHierarchy<TurnPipelineRunner>();

            builder.Register<VisualPipeline>(Lifetime.Singleton);
            builder.RegisterEntryPoint<MoveVisualHandler>();
            builder.RegisterEntryPoint<DestroyVisualHandler>();
            builder.RegisterEntryPoint<AttackVisualHandler>();
            builder.RegisterEntryPoint<DealDamageVisualHandler>();
        }
    }
}