using Lessons.Game;
using UnityEngine;

namespace Lessons.Entities.Common.Components
{
    public sealed class SFXComponent
    {
        private readonly SFX _sfx;

        public SFXComponent(SFX sfx)
        {
            _sfx = sfx;
        }

        public bool TryGetStartTrunSound(out AudioClip clip) => RandomItem(_sfx.StartTurnSounds, out clip);
        public bool TryGetLowHealthSound(out AudioClip clip) => RandomItem(_sfx.LowHealthSounds, out clip);
        public bool TryGetUseAbilitySound(out AudioClip clip) => RandomItem(_sfx.UseAbilitySounds, out clip);
        public bool TryGetDeathSound(out AudioClip clip) => RandomItem(_sfx.DeathSounds, out clip);

        private bool RandomItem<T>(T[] array, out T value)
        {
            if (array.Length == 0)
            {
                value = default;
                return false;
            }

            value = array[Random.Range(0, array.Length)];
            return true;
        }
    }
}