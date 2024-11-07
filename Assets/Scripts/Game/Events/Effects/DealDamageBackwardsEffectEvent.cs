using System;
using Entities;

namespace Lessons.Game.Events.Effects
{
    [Serializable]
    public struct DealDamageBackwardsEffectEvent : IEffect
    {
        public IEntity Source { get; set; }
        public IEntity Target { get; set; }
    }
}