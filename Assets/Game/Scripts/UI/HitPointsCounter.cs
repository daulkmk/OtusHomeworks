using Atomic.Elements;
using Atomic.Objects;
using TMPro;
using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Components.UI
{
    public class HitPointsCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        [Inject]
        private void Construct([Inject(Id = ObjectType.Player)] IAtomicEntity player)
        {
            var hp = player.Get<IAtomicValueObservable<int>>(LifeAPI.Health);
            hp.Subscribe(OnAmmoChanged);

            OnAmmoChanged(hp.Value);
        }

        private void OnAmmoChanged(int value)
        {
            _text.text = "Health: " + value.ToString();
        }
    }
}