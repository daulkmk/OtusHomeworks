using System;
using Game.GameEngine.Mechanics;
using VContainer.Unity;

namespace Lessons.MetaGame.Inventory
{
    public sealed class EquipmentEffectsApplier : IInitializable, IDisposable
    {
        private readonly IComponent_Effector _effector;
        private readonly IEquipment _equipment;

        public EquipmentEffectsApplier(IEquipment equipment, IComponent_Effector effector)
        {
            _effector = effector;
            _equipment = equipment;
        }

        void IInitializable.Initialize()
        {
            _equipment.OnItemEquipped += OnItemEquipped;
            _equipment.OnItemUnequipped += OnItemUnequipped;
        }

        private void OnItemEquipped(EquipSlot _, InventoryItem item)
        {
            if (IsEffectible(item))
            {
                var effect = GetEffect(item);
                _effector.Apply(effect);
            }
        }

        private void OnItemUnequipped(EquipSlot _, InventoryItem item)
        {
            if (IsEffectible(item))
            {
                var effect = GetEffect(item);
                _effector.Discard(effect);
            }
        }

        void IDisposable.Dispose()
        {
            _equipment.OnItemEquipped -= OnItemEquipped;
            _equipment.OnItemUnequipped -= OnItemUnequipped;
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