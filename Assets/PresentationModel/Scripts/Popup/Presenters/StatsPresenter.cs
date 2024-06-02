using UniRx;

namespace ShootEmUp.PresentationModel
{
    public class StatsPresenter : IStatsPresenter
    {
        private readonly CharacterInfoPresenter _characterInfoPresenter;

        public IReadOnlyReactiveCollection<IStatPresenter> Stats => _characterInfoPresenter.Stats;

        public StatsPresenter(CharacterInfoPresenter characterInfoPresenter)
        {
            _characterInfoPresenter = characterInfoPresenter;
        }
    }
}