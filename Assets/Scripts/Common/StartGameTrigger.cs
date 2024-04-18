using Zenject;

namespace ShootEmUp
{
    public class StartGameTrigger : IInitializable
    {
        private readonly IGameManager _gameManager;
        private readonly UI.IGameGUI _gameGUI;

        public StartGameTrigger(UI.IGameGUI gameGUI, IGameManager gameManager)
        {
            _gameManager = gameManager;
            _gameGUI = gameGUI;
        }

        void IInitializable.Initialize()
        {
            _gameGUI.ShowStartGameSequence(OnGuiStartSequenceComplete);
        }

        private void OnGuiStartSequenceComplete()
        {
            _gameManager.State = GameState.Playng;
        }
    }
}