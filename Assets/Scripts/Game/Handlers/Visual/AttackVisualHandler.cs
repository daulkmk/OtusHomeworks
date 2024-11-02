using Lessons.Entities.Common.Components;
using Lessons.Game.Events;
using Lessons.Game.Turn.Visual;
using Lessons.Game.Turn.Visual.Tasks;
using UnityEngine;

namespace Lessons.Game.Handlers.Visual
{
    public sealed class AttackVisualHandler : BaseHandler<AttackEvent>
    {
        private readonly VisualPipeline _visualPipeline;
        
        public AttackVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
        }

        protected override void HandleEvent(AttackEvent evt)
        {
            Vector3 selfPos = evt.Entity.Get<TransformComponent>().Value.position;
            Vector3 targetPos = evt.Target.Get<TransformComponent>().Value.position;

            Vector3 attackDestination = selfPos + (targetPos - selfPos) * 0.5f;
            
            _visualPipeline.AddTask(new AttackVisualTask(evt.Entity, attackDestination));
        }
    }
}