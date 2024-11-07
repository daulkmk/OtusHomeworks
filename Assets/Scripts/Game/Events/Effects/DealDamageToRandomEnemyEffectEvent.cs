using System;
using Entities;
using UnityEngine;

namespace Lessons.Game.Events.Effects
{
    [Serializable]
    public struct DealDamageToRandomEnemyEffectEvent : IEffect
    {
        public IEntity Source { get; set; }
        public IEntity Target { get; set; }
        
        [field:SerializeField]
        public int Damage { get; private set; }
    }
}