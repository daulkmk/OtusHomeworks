using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Lessons.Game.Turn.Logic.Tasks
{
    public sealed class FinishTurnTask : Task
    {
        protected override UniTask OnRun()
        {
            Debug.Log("Pipeline Finished!");

            return UniTask.CompletedTask;
        }
    }
}