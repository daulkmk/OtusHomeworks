using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Lessons.Architecture.PM;
using Sirenix.Utilities;
using UniRx;

namespace ShootEmUp.PresentationModel
{
    public class CharacterInfoPresenter : IDisposable
    {
        public IReadOnlyReactiveCollection<IStatPresenter> Stats => _stats;

        private readonly ReactiveCollection<IStatPresenter> _stats;

        private readonly CharacterInfo _characterInfo;

        public CharacterInfoPresenter(CharacterInfo characterInfo)
        {
            _characterInfo = characterInfo;

            _stats = new ReactiveCollection<IStatPresenter>(_characterInfo.GetStats().Convert(CreateStatPresenter));

            _characterInfo.OnStatAdded += OnStatAdded;
            _characterInfo.OnStatRemoved += OnStatRemoved;
        }

        private void OnStatRemoved(CharacterStat stat)
        {
            for (int i = 0; i < _stats.Count; i++)
            {
                if (_stats[i].Name.Value == stat.Name)
                {
                    var StatPresenter = _stats[i];
                    _stats.RemoveAt(i);

                    StatPresenter.Dispose();
                }
            }
        }

        private void OnStatAdded(CharacterStat stat)
        {
            _stats.Add(CreateStatPresenter(stat));
        }

        private IStatPresenter CreateStatPresenter(object stat) => new StatPresenter((CharacterStat)stat);

        public void Dispose()
        {
            _characterInfo.OnStatAdded -= OnStatAdded;
            _characterInfo.OnStatRemoved -= OnStatRemoved;

            foreach (var statPresenter in _stats)
                statPresenter.Dispose();
        }
    }
}