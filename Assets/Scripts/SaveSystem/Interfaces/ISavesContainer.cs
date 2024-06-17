using Cysharp.Threading.Tasks;

namespace SaveLoad
{
    public interface ISavesContainer
    {
        public UniTask Save<T>(string key, int saveNumb, T obj);
        public UniTask<T> Load<T>(string key, int saveNumb, T defaultValue = default);
        public UniTask<bool> ContainsKey(string key, int saveNumb);
        public UniTask Delete(string key, int saveNumb);
        public UniTask DeleteAll();
    }
}