using System;
using UnityEngine;

namespace Lessons.Game
{
    [Serializable]
    public class SFX
    {
        public AudioClip[] StartTurnSounds;
        public AudioClip[] LowHealthSounds;
        public AudioClip[] UseAbilitySounds;
        public AudioClip[] DeathSounds;
    }
}