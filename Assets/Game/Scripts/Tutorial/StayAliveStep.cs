using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Lessons.Lesson_Components.UI;

namespace Tutorial
{
    public class StayAliveStep : TutorialStep, IDisposable
    {
        private readonly float _duration;
        private CancellationTokenSource _cts = new();

        public StayAliveStep(TutorialScreen tutorialScreen, float duration)
            : base(tutorialScreen)
        {
            _duration = duration;
        }

        public override Step Step => Step.StayAlive;

        public override void Start()
        {
            _tutorialScreen.Show("Stay alive and good luck!");

            //Здесь был бы вызов паузы и ожидание клика по кнопке, но пауза не была реализована в рамках ДЗ.
            //Просто временное сообщение, иначе пришлось бы дорабатывать все механики...
            CompleteAfterTime(_cts.Token).Forget();
        }

        private async UniTaskVoid CompleteAfterTime(CancellationToken cancellationToken)
        {
            await UniTask.WaitForSeconds(_duration, cancellationToken: cancellationToken);
            FireCompleteEvent();
        }

        public override void End()
        {
            _tutorialScreen.Hide();
            _cts?.Cancel();
        }

        void IDisposable.Dispose()
        {
            if (_cts != null)
            {
                if (!_cts.IsCancellationRequested)
                    _cts.Cancel();

                _cts.Dispose();
                _cts = null;
            }
        }
    }
}