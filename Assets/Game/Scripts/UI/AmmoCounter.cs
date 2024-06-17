using Atomic.Elements;
using Atomic.Objects;
using TMPro;
using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Components.UI
{
    public class AmmoCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        [Inject]
        private void Construct([Inject(Id = ObjectType.Player)] IAtomicEntity player)
        {
            var ammo = player.Get<IAtomicVariableObservable<int>>(ShootAPI.Ammo);
            ammo.Subscribe(OnAmmoChanged);

            OnAmmoChanged(ammo.Value);
        }

        private void OnAmmoChanged(int value)
        {
            _text.text = "AMMO: " + value.ToString();
        }
    }
}