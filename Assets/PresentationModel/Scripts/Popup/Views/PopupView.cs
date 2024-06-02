using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.PresentationModel
{
    public class PopupView : MonoBehaviour, IPopupView
    {
        [SerializeField] private DescriptionView _descriptionView;
        [SerializeField] private ExpProgressBarView _expProgressBar;
        [SerializeField] private ButtonView _levelUpButton;
        [SerializeField] private StatsView _statsView;

        [field: SerializeField] public Button CloseButton { get;  private set; }
        
        public GameObject GameObject => gameObject;
        public IProgressBarView ExpProgressBarView => _expProgressBar;
        public IDescriptionView DescriptionView => _descriptionView;
        public IStatsView StatsView => _statsView;
        public IButtonView LevelUpButtonView => _levelUpButton;
    }
}