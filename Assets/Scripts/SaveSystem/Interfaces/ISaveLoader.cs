using Cysharp.Threading.Tasks;

namespace SaveLoad
{
    public interface ISaveLoader
    {
        UniTask Save();
        UniTask Load();
    }
}