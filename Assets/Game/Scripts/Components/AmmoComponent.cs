using System;
using Atomic.Elements;

namespace Lessons.Lesson_Components.Components
{
    [Serializable]
    public class AmmoComponent
    {
        public AtomicVariable<int> Bullets = new();

        public bool HasBullets() => Bullets.Value > 0;
    }
}