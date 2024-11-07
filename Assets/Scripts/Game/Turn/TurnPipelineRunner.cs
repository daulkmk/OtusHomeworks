using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Lessons.Game.Turn
{
    public sealed class TurnPipelineRunner : IInitializable, IDisposable
    {
        private readonly TurnPipeline _turnPipeline;
        private readonly CancellationTokenSource _cts = new();

        public TurnPipelineRunner(TurnPipeline turnPipeline)
        {
            _turnPipeline = turnPipeline;
        }

        void IInitializable.Initialize()
        {
            Run().Forget();
        }

        void IDisposable.Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }

        private async UniTask Run()
        {
            await UniTask.NextFrame();

            while (!_cts.IsCancellationRequested)
            {
                await _turnPipeline.Run().AttachExternalCancellation(_cts.Token);
            }
        }
    }
}