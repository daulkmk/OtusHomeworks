using System;
using Cysharp.Threading.Tasks;

namespace Lessons.Game.Turn
{
    public abstract class Task
    {        
        public async UniTask Run()
        {            
            await OnRun();
            await OnFinish();
        }

        protected abstract UniTask OnRun();

        protected virtual UniTask OnFinish() => UniTask.CompletedTask;
    }
}