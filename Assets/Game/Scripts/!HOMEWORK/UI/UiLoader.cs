using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace SampleGame
{
    public class UiLoader : IInitializable, IDisposable
    {
        private readonly AssetReferenceT<GameObject> _uiAssetReference;
        private CancellationTokenSource _cts = new();
        private readonly Transform _uiContainer;
        private readonly DiContainer _container;
        private AsyncOperationHandle<GameObject> _loadedUiHandle;

        public UiLoader(AssetReferenceT<GameObject> uiAssetReference, Transform uiContainer, DiContainer container)
        {
            _uiAssetReference = uiAssetReference;
            _uiContainer = uiContainer;
            _container = container;
        }

        void IDisposable.Dispose()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }

            if (_loadedUiHandle.IsValid())
            {
                Debug.Log("UNLOAD UI: " + _loadedUiHandle.Result, _loadedUiHandle.Result);
                Addressables.Release(_loadedUiHandle);
            }
        }

        async void IInitializable.Initialize()
        {
            var uiHandle = Addressables.LoadAssetAsync<GameObject>(_uiAssetReference);
            await uiHandle.ToUniTask(cancellationToken: _cts.Token, autoReleaseWhenCanceled: true);

            _loadedUiHandle = uiHandle;
            
            var uiObject = GameObject.Instantiate(_loadedUiHandle.Result, _uiContainer);
            _container.InjectGameObject(uiObject);

            Debug.Log("UI LOADED: " + uiObject, uiObject);
        }
    }
}