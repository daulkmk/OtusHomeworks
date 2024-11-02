using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Lessons.Game.Turn
{
    public abstract class Pipeline
    {        
        private readonly List<Task> _tasks = new();

        public void AddTask(Task task)
        {
            _tasks.Add(task);
        }

        public void ClearTasks()
        {
            _tasks.Clear();
        }

        public async UniTask Run()
        {
            foreach (var task in _tasks)
                await task.Run();
        }
    }
}