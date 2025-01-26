using Cysharp.Threading.Tasks;

namespace SampleGame
{
    public interface ISceneLoader
    {
        bool IsLoadingInProgress { get; }
        UniTask LoadScene(object sceneKey);
    }
}