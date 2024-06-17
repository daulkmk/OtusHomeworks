using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

namespace SaveLoad
{
    //File structure: PersistentDataPath/SavesRoot/Slot/key.bytes
    public class LocalFilesContainer : ISavesContainer
    {
        private const string s_saveFileExtension = ".bytes";
        private const string s_savesRootDirectory = "Saves";

        private readonly ICryptographyService _cryptographyService = null;

        public LocalFilesContainer(ICryptographyService cryptographyService)
        {
            _cryptographyService = cryptographyService;
        }

        public UniTask<bool> ContainsKey(string key, int saveSlot)
        {
            string filePath = GetFilePath(key, saveSlot);
            bool fileExists = File.Exists(filePath);
            return UniTask.FromResult(fileExists);
        }

        public async UniTask Delete(string key, int saveSlot)
        {
            if (await ContainsKey(key, saveSlot))
            {
                var filePath = GetFilePath(key, saveSlot);
                File.Delete(filePath);

                var slotDirectory = GetSlotDirectoryPath(saveSlot);
                if (IsDirectoryEmpty(slotDirectory))
                    Directory.Delete(slotDirectory);
            }
        }

        public UniTask DeleteAll()
        {
            var root = GetSaveRootDirectoryPath();
            
            if (Directory.Exists(root))
                Directory.Delete(root, true);

            return UniTask.CompletedTask;
        }

        public async UniTask<T> Load<T>(string key, int saveSlot, T defaultValue = default)
        {
            if (await ContainsKey(key, saveSlot))
            {
                var filePath = GetFilePath(key, saveSlot);
                var bytes = await File.ReadAllBytesAsync(filePath);

                bytes = await _cryptographyService.Decrypt(bytes);

                return ByteArrayToObject<T>(bytes);
            }

            return defaultValue;
        }

        public async UniTask Save<T>(string key, int saveSlot, T obj)
        {
            MakeSureSaveSlotDirectoryExists(saveSlot);

            var bytes = ObjectToByteArray(obj);

            bytes = await _cryptographyService.Encrypt(bytes);
            
            var filePath = GetFilePath(key, saveSlot);
            await File.WriteAllBytesAsync(filePath, bytes);
        }

        private static string KeyToFileName(string key) => key + s_saveFileExtension;

        public static string GetFilePath(string key, int saveSlot)
        {
            return Path.Combine(GetSlotDirectoryPath(saveSlot), KeyToFileName(key));
        }
        private static string GetSlotDirectoryPath(int saveSlot)
        {
            return Path.Combine(GetSaveRootDirectoryPath(), saveSlot.ToString());
        }
        private static string GetSaveRootDirectoryPath() 
        {
            return Path.Combine(Application.persistentDataPath, s_savesRootDirectory);
        }

        private static void MakeSureSaveSlotDirectoryExists(int saveSlot)
        {
            var root = GetSaveRootDirectoryPath();
            CreateDirectoryIfNotExists(root);

            var slot = GetSlotDirectoryPath(saveSlot);
            CreateDirectoryIfNotExists(slot);
        }
        private static void CreateDirectoryIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }
        private static bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        private static byte[] ObjectToByteArray<T>(T obj)
        {
            string json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            var bf = new BinaryFormatter();
            using var ms = new MemoryStream();
        
            bf.Serialize(ms, json);
            return ms.ToArray();
        }

        private T ByteArrayToObject<T>(byte[] arrBytes)
        {
            var binForm = new BinaryFormatter();
            using var memStream = new MemoryStream();

            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);

            var json = (string)binForm.Deserialize(memStream);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}