using System;
using Lessons.Game.Events.Effects;
using UnityEngine;

namespace Lessons.Game
{
    [Serializable]
    public class Weapon
    {
        [SerializeReference]
        public IEffect[] Effects;
    }
}