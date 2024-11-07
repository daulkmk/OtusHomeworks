using Cysharp.Threading.Tasks;
using DG.Tweening;
using Entities;
using Lessons.Entities.Common.Components;
using UnityEngine;

namespace Lessons.Game.Turn.Visual.Tasks
{
    public sealed class DestroyVisualTask : Task
    {
        private readonly AudioPlayer _audioPlayer;
        private readonly IEntity _entity;
        private readonly float _duration;

        public DestroyVisualTask(IEntity entity, AudioPlayer audioPlayer, float duration = 0.15f)
        {
           _entity = entity;
            _audioPlayer = audioPlayer;
            _duration = duration;
        }

        protected override async UniTask OnRun()
        {
            if (_entity.TryGet(out SFXComponent sfx) && sfx.TryGetDeathSound(out var clip))
            {
                _audioPlayer.PlaySound(clip);
                await UniTask.WaitForSeconds(clip.length);
            }

            var transform = _entity.Get<TransformComponent>();

            await DOTween.Sequence()
                .Append(transform.Value.DOScale(Vector3.zero, _duration))
                .AppendCallback(() => transform.Value.gameObject.SetActive(false))
                .ToUniTask();
        }
    }
}