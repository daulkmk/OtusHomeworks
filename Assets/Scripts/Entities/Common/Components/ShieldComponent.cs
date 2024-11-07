using Lessons.Game;
using Lessons.Utils;

namespace Lessons.Entities.Common.Components
{
    public sealed class ShieldComponent
    {
        public AtomicVariable<int> AttacksToBlock => _shield.Value.AttacksToBlock;
        
        private readonly AtomicVariable<Shield> _shield;

        public ShieldComponent(AtomicVariable<Shield> shield)
        {
            _shield = shield;
        }
    }
}