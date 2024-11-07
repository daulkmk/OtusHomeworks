using System;
using Lessons.Utils;

namespace Lessons.Game
{
    [Serializable]
    public class Shield
    {
        public AtomicVariable<int> AttacksToBlock;
    }
}