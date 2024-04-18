using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class DamagableProxy : MonoBehaviour, IDamagable
    {
        private IDamagable[] _damagables;

        [Inject]
        private void Construct(IDamagable[] damagables)
        {
            _damagables = damagables;
        }

        public void ApplyDamage(int damage, bool isPlayer)
        {
            foreach (var damagable in _damagables)
                damagable.ApplyDamage(damage, isPlayer);
        }
    }
}