using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace SampleGame
{
    public class LocationsLoader : ILocationsLoader, IDisposable
    {
        private readonly List<AsyncOperationHandle<GameObject>> _locationsHandles = new();
        private readonly HashSet<object> _loadedLocations = new();
        private readonly Transform _container;
        private readonly DiContainer _diContainer;

        private CancellationTokenSource _cts = new();

        public LocationsLoader(Transform container, DiContainer diContainer)
        {
            _container = container;
            _diContainer = diContainer;
        }

        public async UniTask LoadLocation(AssetReferenceT<GameObject> locationReference)
        {
            if (_loadedLocations.Contains(locationReference.RuntimeKey))
            {
#if UNITY_EDITOR
                Debug.Log("LOCATION ALREADY LOADED: " + locationReference.editorAsset, locationReference.editorAsset);
#endif
                return;
            }

            _loadedLocations.Add(locationReference.RuntimeKey);

            var handle = Addressables.LoadAssetAsync<GameObject>(locationReference);
            
            await handle.ToUniTask(cancellationToken: _cts.Token, autoReleaseWhenCanceled: true);
            _locationsHandles.Add(handle);

            var location = GameObject.Instantiate(handle.Result, _container);
            _diContainer.InjectGameObject(location);

            Debug.Log("LOCATION LOADED: " + location, location);
        }

        void IDisposable.Dispose()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }

            if (_locationsHandles.Count > 0)
            {
                foreach (var handle in _locationsHandles)
                {
                    if (handle.IsValid())
                    {
                        Debug.Log("UNLOAD LOCATION: " + handle.Result, handle.Result);
                        Addressables.Release(handle);
                    }
                }
                _locationsHandles.Clear();
            }
        }
    }
}