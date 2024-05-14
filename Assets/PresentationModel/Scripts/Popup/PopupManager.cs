using Lessons.Architecture.PM;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

using CharacterInfo = Lessons.Architecture.PM.CharacterInfo;

namespace ShootEmUp.PresentationModel
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField] private IPopup _popup;

        private PopupPresetnerFactory _popupPresetnerFactory;
        private (UserInfo, CharacterInfo, PlayerLevel)[] _characters;

        [Inject]
        private void Construct(IPopup popup, PopupPresetnerFactory popupPresetnerFactory, (UserInfo, CharacterInfo, PlayerLevel)[] characters)
        {
            _popup = popup;
            _popupPresetnerFactory = popupPresetnerFactory;
            _characters = characters;
        }

        private void Awake()
        {
            Hide();
        }

        [Button]
        public void ShowPopupWithCharacterByIndex(int characterIndex)
        {
            var (userInfo, characterInfo, playerLevel) = _characters[characterIndex];

            var presenter = _popupPresetnerFactory.Create(characterInfo, playerLevel, userInfo);
            _popup.Show(presenter);
        }

        [Button]
        public void Hide()
        {
            _popup.Hide();
        }
    }
}