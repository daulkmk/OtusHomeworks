using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Lessons.MetaGame.Inventory
{
    public sealed class SceneLifetimeScope : LifetimeScope
    {
        [SerializeField] private Player _player;
        [SerializeField] private ListInventoryPresenter _inventoryPresenter;
        [SerializeField] private EquipmentPresenter _equipmentPresenter;

        [SerializeField] private List<InventoryItemConfig> _startItems = new();
        
        [ShowInInspector] private readonly ListInventory _inventory = new();
        [ShowInInspector] private readonly Equipment _equipment = new();

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance<Player>(_player);
            builder.RegisterInstance<Equipment>(_equipment);
            builder.RegisterInstance<ListInventory>(_inventory);

            SetupGame();
        }

        void SetupGame()
        {
            foreach (var itemConfig in _startItems)
            {
                var itemClone = itemConfig.item.Clone();
                _inventory.AddItem(itemClone);
            }

            var playerEffector = new PlayerEffector(_player);
            var effectsApplier = new InventoryEffectsApplier(playerEffector);

            _equipment.AddObserver(effectsApplier);

            _inventoryPresenter.OnItemClicked += x => InventoryEqupmentUseCases.EquipItem(x, _inventory, _equipment);
            _equipmentPresenter.OnItemClicked += x => InventoryEqupmentUseCases.UnequipItem(x, _inventory, _equipment);
        }

#region Debug
        [Button]
        private void AddItem(InventoryItemConfig config)
        {
            var prefab = config.item;
            var inventoryItem = prefab.Clone();
            _inventory.AddItem(inventoryItem);
        }

        [Button]
        private void RemoveItem(InventoryItemConfig config)
        {
            _inventory.RemoveItem(config.item.Name);
        }
#endregion

    }
}