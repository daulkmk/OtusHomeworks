using Cysharp.Threading.Tasks;

namespace SaveLoad
{
    public class GameRepository : IGameRepository
    {
        /// <summary>
        /// Default save slot.
        /// </summary>
        private const int s_saveSlot = 1;
        
        private readonly ISavesContainer _savesContainer;

        //SavesContainer is abstraction for file storage, server or anything else.
        //Now there is only on-device files container LocalFilesContainer
        public GameRepository(ISavesContainer savesConainer)
        {
            _savesContainer = savesConainer;
        }

        public UniTask Save<T>(string key, T obj) 
        { 
            return _savesContainer.Save(key, s_saveSlot, obj);
        }
        
        public UniTask<T> Load<T>(string key, T defaultValue = default) 
        { 
            return _savesContainer.Load(key, s_saveSlot, defaultValue);
        }

        public UniTask<bool> ContainsKey(string key) 
        { 
            return _savesContainer.ContainsKey(key, s_saveSlot);
        }

        public UniTask Delete(string key) 
        { 
            return _savesContainer.Delete(key, s_saveSlot);  
        }

        public UniTask DeleteAll()
        {
            return _savesContainer.DeleteAll();
        }
    }
}