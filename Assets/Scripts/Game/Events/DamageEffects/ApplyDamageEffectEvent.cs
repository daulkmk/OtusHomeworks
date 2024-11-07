using System;
using Entities;

namespace Lessons.Game.Events.Effects
{
    [Serializable]
    public struct ApplyDamageEffectEvent : IDamageEffect
    {
        public IEntity Source { get; set; }
        public IEntity Target { get; set; }
        public int Damage { get; set; }
    }
}