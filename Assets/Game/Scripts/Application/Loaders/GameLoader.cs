using Cysharp.Threading.Tasks;


namespace SampleGame
{
    public sealed class GameLoader
    {
        private readonly object _gameSceneKey;
        private readonly ISceneLoader _sceneLoader;

        public GameLoader(ISceneLoader sceneLoader, object gameSceneKey)
        {
            _gameSceneKey = gameSceneKey;
            _sceneLoader = sceneLoader;
        }
        
        public void LoadGame()
        {
            if (!_sceneLoader.IsLoadingInProgress)
            {
                _sceneLoader.LoadScene(_gameSceneKey).Forget();
            }
        }
    }
}