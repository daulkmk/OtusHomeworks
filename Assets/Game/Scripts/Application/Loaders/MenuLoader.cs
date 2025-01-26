using Cysharp.Threading.Tasks;

namespace SampleGame
{
    public sealed class MenuLoader
    {
        private readonly object _menuSceneKey;
        private readonly ISceneLoader _sceneLoader;

        public MenuLoader(ISceneLoader sceneLoader, object gameSceneKey)
        {
            _menuSceneKey = gameSceneKey;
            _sceneLoader = sceneLoader;
        }
        
        public void LoadMenu()
        {
            if (!_sceneLoader.IsLoadingInProgress)
            {
                _sceneLoader.LoadScene(_menuSceneKey).Forget();
            }
        }
    }
}