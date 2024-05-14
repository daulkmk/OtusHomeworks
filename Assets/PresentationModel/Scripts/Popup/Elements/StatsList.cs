using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.PresentationModel
{
    public class StatsList : MonoBehaviour
    {
        [SerializeField] PopupStatView _statViewPrefab;

        private readonly List<PopupStatView> _statViews = new();
        private readonly List<PopupStatView> _viewsPool = new();
        
        public void Show(IStatPresenter statPresenter)
        {
            var view = GetPooledOrCreateView();
            
            _statViews.Add(view);

            view.Show(statPresenter);
        }

        public void Hide(IStatPresenter statPresenter)
        {
            var viewIndex = _statViews.FindIndex(v => v.Presenter == statPresenter);
            if (viewIndex == -1)
                return;
            
            var view = _statViews[viewIndex];

            view.Hide();

            _viewsPool.Add(view);
            _statViews.RemoveAt(viewIndex);
        }

        public void Clear()
        {
            _viewsPool.AddRange(_statViews);

            _statViews.ForEach(v => v.Hide());
            _statViews.Clear();
        }

        private PopupStatView GetPooledOrCreateView()
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