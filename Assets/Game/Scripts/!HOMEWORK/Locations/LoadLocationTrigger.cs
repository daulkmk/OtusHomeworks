using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace SampleGame
{
    public class LoadLocationTrigger : MonoBehaviour
    {
        [SerializeField] private AssetReferenceT<GameObject> _locationReference;
        private ILocationsLoader _locationsLoader;

        [Inject]
        private void Construct(ILocationsLoader locationsLoader)
        {
            _locationsLoader = locationsLoader;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Character>(out _))
            {
                _locationsLoader.LoadLocation(_locationReference);
            }
        }
    }
}