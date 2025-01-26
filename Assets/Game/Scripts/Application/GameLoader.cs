using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace SampleGame
{
    public sealed class GameLoader
    {
        private readonly object _gameSceneKey;

        private AsyncOperationHandle _sceneHandle;

        public GameLoader(object gameSceneKey)
        {
            _gameSceneKey = gameSceneKey;
        }

        public void UnloadGame()
        {
            Addressables.UnloadSceneAsync(_sceneHandle);
        }
        
        public void LoadGame()
        {
            if (_sceneHandle.IsValid())
                return;

            _sceneHandle = Addressables.LoadSceneAsync(_gameSceneKey);
        }
    }
}