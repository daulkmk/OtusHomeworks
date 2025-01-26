using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace SampleGame
{
    public class SingleSceneLoader : ISceneLoader, IDisposable
    {
        private AsyncOperationHandle<SceneInstance> _loadedSceneHandle;
        private CancellationTokenSource _cts = new();

        public bool IsLoadingInProgress { get; private set; }

        public void Dispose()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }

        public async UniTask LoadScene(object sceneKey)
        {
            if (IsLoadingInProgress)
                throw new Exception("Already loading some scene!");

            IsLoadingInProgress = true;
            await UnloadActiveScene();

            _loadedSceneHandle = Addressables.LoadSceneAsync(sceneKey, LoadSceneMode.Single);
            await _loadedSceneHandle;

            string loadedSceneName = _loadedSceneHandle.Result.Scene.name;
            Debug.Log("SCENE LOADED: " + loadedSceneName);

            IsLoadingInProgress = false;
        }

        private async UniTask UnloadActiveScene()
        {
            if (_loadedSceneHandle.IsValid())
            {
                string unloadedSceneName = _loadedSceneHandle.Result.Scene.name;

                await Addressables.UnloadSceneAsync(_loadedSceneHandle);
                _loadedSceneHandle = default;

                Debug.Log("SCENE UNLOADED: " + unloadedSceneName);
            }
        }
    }
}