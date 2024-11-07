using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;
using Lessons.Game.Services;

namespace Lessons.Game.Handlers.Visual
{
    [UsedImplicitly]
    public sealed class TurnStartedHandler : BaseHandler<TurnStartedEvent>
    {
        private readonly PlayersService _playersService;
        private readonly AudioPlayer _audioPlayer;

        public TurnStartedHandler(EventBus eventBus, PlayersService playersService, AudioPlayer audioPlayer)
             : base(eventBus)
        {
            _playersService = playersService;
            _audioPlayer = audioPlayer;
        }

        protected override void HandleEvent(TurnStartedEvent evt)
        {
            var hero = _playersService.PlayerHero;

            if (hero.TryGet(out SFXComponent sfx) && sfx.TryGetStartTrunSound(out var clip))
                _audioPlayer.PlaySound(clip);
        }
    }
}