using Cysharp.Threading.Tasks;
using DG.Tweening;
using Entities;
using Lessons.Entities.Common.Components;
using UnityEngine;

namespace Lessons.Game.Turn.Visual.Tasks
{
    public sealed class DealDamageVisualTask : Task
    {
        private readonly TransformComponent _transform;
        private readonly float _duration;

        public DealDamageVisualTask(IEntity entity, float duration = 0.15f)
        {
            _transform = entity.Get<TransformComponent>();
            _duration = duration;
        }

        protected override UniTask OnRun()
        {
            return _transform.Value.DOScale(Vector3.one * 1.1f, _duration)
                .SetLoops(2, LoopType.Yoyo)
                .ToUniTask();
        }
    }
}