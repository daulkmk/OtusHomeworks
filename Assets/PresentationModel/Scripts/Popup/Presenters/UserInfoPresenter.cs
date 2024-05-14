using System;
using Lessons.Architecture.PM;
using UniRx;

namespace ShootEmUp.PresentationModel
{
    public class UserInfoPresenter : IDisposable
    {
        public IReadOnlyReactiveProperty<string> Name => _name;
        public IReadOnlyReactiveProperty<string> Description => _description;
        public IReadOnlyReactiveProperty<UnityEngine.Sprite> Icon => _icon;

        private readonly UserInfo _userInfo;
        private readonly ReactiveProperty<string> _name;
        private readonly ReactiveProperty<string> _description;
        private readonly ReactiveProperty<UnityEngine.Sprite> _icon;

        public UserInfoPresenter(UserInfo userInfo)
        {
            _userInfo = userInfo;

            _name = new ReactiveProperty<string>(_userInfo.Name);
            _userInfo.OnNameChanged += OnNameChanged;

            _description = new ReactiveProperty<string>(_userInfo.Description);
            _userInfo.OnDescriptionChanged += OnDescriptionChanged;

            _icon = new ReactiveProperty<UnityEngine.Sprite>(_userInfo.Icon);
            _userInfo.OnIconChanged += OnIconChanged;
        }

        private void OnIconChanged(UnityEngine.Sprite sprite) => _icon.Value = sprite;
        private void OnDescriptionChanged(string description) => _description.Value = description;
        private void OnNameChanged(string name) => _name.Value = name;

        public void Dispose()
        {
            _userInfo.OnNameChanged -= OnNameChanged;
            _userInfo.OnDescriptionChanged -= OnDescriptionChanged;
            _userInfo.OnIconChanged -= OnIconChanged;
        }
    }
}