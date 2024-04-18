namespace ShootEmUp
{
    public class GameGUIController : IPauseGameListener, IFinishGameListener
    {
        private readonly UI.IGameGUI _gameGUI;

        public GameGUIController(UI.IGameGUI gameGUI)
        {
            _gameGUI = gameGUI;
        }

        void IPauseGameListener.OnGamePaused(bool paused)
        {
            if (paused)
                _gameGUI.ShowPause();
            else
                _gameGUI.ShowGame();
        }

        void IFinishGameListener.OnGameFinished()
        {
            _gameGUI.ShowGameOver();
        }
    }
}