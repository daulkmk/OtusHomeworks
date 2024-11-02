using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Lessons.Game.Turn.Logic.Tasks
{
    public sealed class StartTurnTask : Task
    {
        protected override UniTask OnRun()
        {
            Debug.Log("Pipeline Started!");
            return UniTask.CompletedTask;
        }
    }
}