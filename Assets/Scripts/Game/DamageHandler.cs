using System;
using Lessons.Game.Events.Effects;
using UnityEngine;
using UnityEngine.UIElements;

namespace Lessons.Game
{
    [Serializable]
    public class DamageHandler
    {
        [SerializeReference]
        public IDamageEffect[] Effects = new IDamageEffect[] { new ApplyDamageEffectEvent() };
    }
}