
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.Utilities;

namespace SaveLoad
{
    public class SaveLoadService
    {
        private readonly IEnumerable<ISaveLoader> _saveLoaders;

        public SaveLoadService(IEnumerable<ISaveLoader> saveLoaders)
        {
            _saveLoaders = saveLoaders;
        }

        public UniTask Save()
        {
            var saveTasks = _saveLoaders.Convert(x => ((ISaveLoader)x).Save());
            return UniTask.WhenAll(saveTasks);
        }

        public UniTask Load()
        {
            var loadTasks = _saveLoaders.Convert(x => ((ISaveLoader)x).Load());
            return UniTask.WhenAll(loadTasks);
        }
    }
}