using System;
using Entities;
using UnityEngine;

namespace Lessons.Game.Events.Effects
{
    [Serializable]
    public struct DealDamageWithRandomTargetEffectEvent : IEffect
    {
        public IEntity Source { get; set; }
        public IEntity Target { get; set; }

        [field: SerializeField, Range(0, 1)]
        public float HintRandomTargetChance { get; private set; }
    }
}