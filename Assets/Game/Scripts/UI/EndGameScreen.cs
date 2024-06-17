using Atomic.Elements;
using Atomic.Objects;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Components.UI
{
    public class EndGameScreen : MonoBehaviour
    {
        [SerializeField] private float _delay = 3;

        [Inject]
        private void Construct([Inject(Id = ObjectType.Player)] IAtomicEntity player)
        {
            var isDead = player.Get<IAtomicValueObservable<bool>>(LifeAPI.IsDead);
            isDead.Subscribe(OnPlayedIsDead);
        }

        private async void OnPlayedIsDead(bool obj)
        {
            var ct = gameObject.GetCancellationTokenOnDestroy();

            await UniTask.WaitForSeconds(_delay, cancellationToken: ct);
            
            gameObject.SetActive(true);
        }
    }
}