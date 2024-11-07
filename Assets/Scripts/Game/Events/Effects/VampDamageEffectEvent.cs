using System;
using Entities;

namespace Lessons.Game.Events.Effects
{
    [Serializable]
    public struct VampDamageEffectEvent : IEffect
    {
        public IEntity Source { get; set; }
        public IEntity Target { get; set; }
    }
}