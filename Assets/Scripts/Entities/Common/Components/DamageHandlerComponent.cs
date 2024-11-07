using Lessons.Game;
using Lessons.Utils;

namespace Lessons.Entities.Common.Components
{
    public sealed class DamageHandlerComponent
    {
        public DamageHandler DamageHandler => _damageHandler.Value;
        private readonly AtomicVariable<DamageHandler> _damageHandler;

        public DamageHandlerComponent(DamageHandler damageHandler)
        {
            _damageHandler = damageHandler;
        }
    }
}