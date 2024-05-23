using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

namespace SaveLoad
{
    public class GameRepository : IGameRepository
    {
        private const string s_saveFileName = ".bites";

        /// <summary>
        /// Default save slot.
        /// </summary>
        private const int s_saveNumb = 1;
        private ICryptographyService _cryptographyService = null;

        public GameRepository(ICryptographyService cryptographyService)
        {
            _cryptographyService = cryptographyService;
        }

        private static string GetFileName(string key) => $"{key}.bites";

        private static string GetSaveDirectoryPath(int saveNumb = s_saveNumb)
        {
            return Path.Combine(Application.persistentDataPath, saveNumb.ToString());
        }

        public static string GetFilePath(string key, int saveNumb = s_saveNumb)
        {
            return Path.Combine(GetSaveDirectoryPath(saveNumb), GetFileName(key));
        }

        public UniTask<bool> ContainsKey(string key)
        {
            return UniTask.FromResult(File.Exists(GetFilePath(key)));
        }

        public async UniTask Delete(string key)
        {
            if (await ContainsKey(key))
            {
                File.Delete(GetFilePath(key));

                var slotDirectory = GetSaveDirectoryPath();
                if (IsDirectoryEmpty(slotDirectory))
                    Directory.Delete(slotDirectory);
            }
        }

        public async UniTask<T> Load<T>(string key, T defaultValue = default)
        {
            if (await ContainsKey(key))
            {
                var bytes = await File.ReadAllBytesAsync(GetFilePath(key));

                return ByteArrayToObject<T>(bytes);
            }

            return defaultValue;
        }

        public async UniTask Save<T>(string key, T obj)
        {
            MakeSureSaveDirectoryExists();

            var bytes = ObjectToByteArray(obj);
            
            await File.WriteAllBytesAsync(GetFilePath(key), bytes);
        }

        private static void MakeSureSaveDirectoryExists(int saveNumb = s_saveNumb)
        {
            var directory = GetSaveDirectoryPath(saveNumb);
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