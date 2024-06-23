using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SaveLoad
{
    //File structure: PersistentDataPath/SavesRoot/Slot/key.bytes
    public class LocalFilesContainer : ISavesContainer
    {
        private const string s_saveFileExtension = ".bytes";
        private const string s_savesRootDirectory = "Saves";

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

                return SaveUtils.ByteArrayToObject<T>(bytes);
            }

            return defaultValue;
        }

        public async UniTask Save<T>(string key, int saveSlot, T obj)
        {
            MakeSureSaveSlotDirectoryExists(saveSlot);

            var bytes = SaveUtils.ObjectToByteArray(obj);
            var filePath = GetFilePath(key, saveSlot);

            await File.WriteAllBytesAsync(filePath, bytes);
        }

        private string KeyToFileName(string key) => key + s_saveFileExtension;

        public string GetFilePath(string key, int saveSlot)
        {
            return Path.Combine(GetSlotDirectoryPath(saveSlot), KeyToFileName(key));
        }
        private string GetSlotDirectoryPath(int saveSlot)
        {
            return Path.Combine(GetSaveRootDirectoryPath(), saveSlot.ToString());
        }
        private string GetSaveRootDirectoryPath()
        {
            return Path.Combine(Application.persistentDataPath, s_savesRootDirectory);
        }

        private void MakeSureSaveSlotDirectoryExists(int saveSlot)
        {
            var root = GetSaveRootDirectoryPath();
            CreateDirectoryIfNotExists(root);

            var slot = GetSlotDirectoryPath(saveSlot);
            CreateDirectoryIfNotExists(slot);
        }
        private void CreateDirectoryIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }
        private bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }
    }
}