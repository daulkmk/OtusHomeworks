using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SampleGame
{
    public interface ILocationsLoader
    {
        UniTask LoadLocation(AssetReferenceT<GameObject> locationReference);
    }
}