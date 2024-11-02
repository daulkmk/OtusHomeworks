using Cysharp.Threading.Tasks;
using DG.Tweening;
using Entities;
using Lessons.Entities.Common.Components;
using UnityEngine;

namespace Lessons.Game.Turn.Visual.Tasks
{
    public sealed class DestroyVisualTask : Task
    {
        private readonly TransformComponent _transform;
        private readonly float _duration;

        public DestroyVisualTask(IEntity entity, float duration = 0.15f)
        {
            _transform = entity.Get<TransformComponent>();
            _duration = duration;
        }
        
        protected override UniTask OnRun()
        {
            return _transform.Value.DOScale(Vector3.zero, _duration).ToUniTask();
        }
    }
}