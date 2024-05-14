using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.PresentationModel
{
    public class PopupView : MonoBehaviour, IPopupView
    {
        [field: SerializeField]
        public Text Name { get; private set; }

        [field: SerializeField] 
        public Text Description { get; private set; }

        [field: SerializeField] 
        public Text Level { get; private set; }

        [field: SerializeField] 
        public Image Icon { get; private set; }

        [field: SerializeField] 
        public ProgressBar ExpProgressBar { get; private set; }

        [field: SerializeField] 
        public LevelUpButton LevelUpButton { get; private set; }

        [field: SerializeField] 
        public StatsList StatsList { get; private set; }

        [field: SerializeField] 
        public Button CloseButton { get; private set; }

        public GameObject GameObject => gameObject;
    }
}