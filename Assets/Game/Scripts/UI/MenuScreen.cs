using SampleGame;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SampleGame
{
    public sealed class MenuScreen : MonoBehaviour
    {
        [SerializeField]
        private Button startButton;

        [SerializeField]
        private Button exitButton;
        
        private ApplicationExiter applicationExiter;
        private GameLoader gameLoader;
        
        [Inject]
        public void Construct(ApplicationExiter applicationFinisher, GameLoader gameLoader)
        {
            this.gameLoader = gameLoader;
            this.applicationExiter = applicationFinisher;

            if (isActiveAndEnabled)
                OnEnable();
        }

        private void OnEnable()
        {
            if (this.gameLoader != null)
                this.startButton.onClick.AddListener(this.gameLoader.LoadGame);
            if (this.applicationExiter != null)
                this.exitButton.onClick.AddListener(this.applicationExiter.ExitApp);
        }

        private void OnDisable()
        {
            this.startButton.onClick.RemoveListener(this.gameLoader.LoadGame);
            this.exitButton.onClick.RemoveListener(this.applicationExiter.ExitApp);
        }
    }
}