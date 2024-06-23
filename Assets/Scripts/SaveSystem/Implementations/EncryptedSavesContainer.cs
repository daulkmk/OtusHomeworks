using Cysharp.Threading.Tasks;

namespace SaveLoad
{
    public class EncryptedSavesContainer : ISavesContainer
    {
        private readonly ICryptographyService _cryptographyService;
        private readonly ISavesContainer _savesContainer;

        public EncryptedSavesContainer(ISavesContainer savesContainer, ICryptographyService cryptographyService)
        {
            _savesContainer = savesContainer;
            _cryptographyService = cryptographyService;
        }

        UniTask<bool> ISavesContainer.ContainsKey(string key, int saveNumb)
        {
            return _savesContainer.ContainsKey(key, saveNumb);
        }

        UniTask ISavesContainer.Delete(string key, int saveNumb)
        {
            return _savesContainer.Delete(key, saveNumb);
        }

        UniTask ISavesContainer.DeleteAll()
        {
            return _savesContainer.DeleteAll();
        }

        async UniTask<T> ISavesContainer.Load<T>(string key, int saveNumb, T defaultValue)
        {
            if (!await _savesContainer.ContainsKey(key, saveNumb))
                return defaultValue;

            var encryptedBytes = await _savesContainer.Load<byte[]>(key, saveNumb, null);
            var bytes = await _cryptographyService.Decrypt(encryptedBytes);

            return SaveUtils.ByteArrayToObject<T>(bytes);
        }

        async UniTask ISavesContainer.Save<T>(string key, int saveNumb, T obj)
        {
            var bytes = SaveUtils.ObjectToByteArray(obj);
            var encryptedBytes = await _cryptographyService.Encrypt(bytes);

            await _savesContainer.Save(key, saveNumb, encryptedBytes);
        }
    }
}