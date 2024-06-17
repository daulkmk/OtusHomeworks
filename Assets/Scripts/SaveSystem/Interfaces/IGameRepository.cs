using Cysharp.Threading.Tasks;

namespace SaveLoad
{
    public interface IGameRepository
    {
        public UniTask Save<T>(string keyl, T obj);
        public UniTask<T> Load<T>(string key, T defaultValue = default);
        public UniTask<bool> ContainsKey(string key);
        public UniTask Delete(string key);
        public UniTask DeleteAll();
    }
}