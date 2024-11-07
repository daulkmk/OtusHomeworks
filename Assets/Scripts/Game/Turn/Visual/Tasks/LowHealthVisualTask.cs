using Cysharp.Threading.Tasks;
using Entities;
using Lessons.Entities.Common.Components;

namespace Lessons.Game.Turn.Visual.Tasks
{
    public sealed class LowHealthVisualTask : Task
    {
        private readonly IEntity _entity;
        private readonly AudioPlayer _audioPlayer;

        public LowHealthVisualTask(IEntity entity, AudioPlayer audioPlayer)
        {
            _entity = entity;
            _audioPlayer = audioPlayer;
        }

        protected override UniTask OnRun()
        {
            if (_entity.TryGet(out SFXComponent sfx) && sfx.TryGetLowHealthSound(out var clip))
            {
                _audioPlayer.PlaySound(clip);
                return UniTask.WaitForSeconds(clip.length);
            }

            return UniTask.CompletedTask;
        }
    }
}