using System;
using Lessons.Game.Events.Effects;
using UnityEngine;

namespace Lessons.Game
{
    [Serializable]
    public class TargetSelector
    {
        [SerializeReference]
        public ISelectTargetEffect Effect = new SelectDefaultTargetEffectEvent();
    }
}