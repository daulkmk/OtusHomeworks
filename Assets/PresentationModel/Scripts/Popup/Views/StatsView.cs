using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ShootEmUp.PresentationModel
{
    public class StatsView : MonoBehaviour, IStatsView
    {
        [SerializeField] private StatView _statViewPrefab;

        private readonly Dictionary<IStatPresenter, StatView> _statViews = new();
        private readonly List<StatView> _viewsPool = new();
        private readonly CompositeDisposable _disposables = new();

        private IStatsPresenter _presenter;

        public void Show(IStatsPresenter statsPresenter)
        {
            Clear();

            _presenter = statsPresenter;

            foreach (var statPresenter in _presenter.Stats)
                Show(statPresenter);

            _presenter.Stats.ObserveAdd().Subscribe(OnStatAdded).AddTo(_disposables);
            _presenter.Stats.ObserveRemove().Subscribe(OnStatRemoved).AddTo(_disposables);
        }

        public void Hide() => Clear();

        private void OnStatAdded(CollectionAddEvent<IStatPresenter> @event) => Show(@event.Value);
        private void OnStatRemoved(CollectionRemoveEvent<IStatPresenter> @event) => Hide(@event.Value);

        private void Show(IStatPresenter statPresenter)
        {
            var view = GetPooledOrCreateView();

            _statViews.Add(statPresenter, view);

            view.Show(statPresenter);
        }

        private void Hide(IStatPresenter statPresenter)
        {
            if (_statViews.TryGetValue(statPresenter, out var view))
                return;

            view.Hide();

            _viewsPool.Add(view);
            _statViews.Remove(statPresenter);
        }

        private void Clear()
        {
            _disposables.Clear();

            foreach (var viewKvp in _statViews)
            {
                var veiw = viewKvp.Value;

                veiw.Hide();
                _viewsPool.Add(veiw);
            }

            _statViews.Clear();
        }

        private StatView GetPooledOrCreateView()
        {
            if (_viewsPool.Count > 0)
            {
                var view = _viewsPool[^1];
                _viewsPool.RemoveAt(_viewsPool.Count - 1);
                view.transform.SetAsLastSibling();

                return view;
            }

            return Instantiate(_statViewPrefab, transform);
        }
    }
}