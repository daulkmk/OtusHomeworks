using Entities;
using Game.GameEngine.Mechanics;

namespace Lessons.MetaGame.Inventory
{
    public sealed class InventoryEffectsApplier : IInventoryObserver
    {
        private readonly IComponent_Effector _effector;

        public InventoryEffectsApplier(IComponent_Effector effector)
        {
            this._effector = effector;
        }

        void IInventoryObserver.OnItemAdded(InventoryItem item)
        {
            if (IsEffectible(item))
            {
                var effect = GetEffect(item);
                _effector.Apply(effect);
            }
        }

        void IInventoryObserver.OnItemRemoved(InventoryItem item)
        {
            if (IsEffectible(item))
            {
                var effect = GetEffect(item);
                _effector.Discard(effect);
            }
        }

        private static IEffect GetEffect(InventoryItem item)
        {
            return item.GetComponent<IComponent_GetEffect>().Effect;
        }

        private static bool IsEffectible(InventoryItem item)
        {
            return item.Flags.HasFlag(InventoryItemFlags.EFFECTIBLE);
        }
    }
}