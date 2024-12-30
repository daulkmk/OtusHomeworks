using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Lessons.MetaGame.Inventory
{
    public sealed class SceneLifetimeScope : LifetimeScope
    {
        [SerializeField] private InventoryItemConfig[] _startItems;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterComponentInHierarchy<Player>();

            builder.RegisterEntryPoint<Equipment>();
            builder.RegisterEntryPoint<ListInventory>()
                .WithParameter( _startItems.Select(x => x.item.Clone()).ToArray());

            builder.RegisterEntryPoint<PlayerEffector>();
            builder.RegisterEntryPoint<EquipmentEffectsApplier>();

            builder.RegisterComponentInHierarchy<ListInventoryPresenter>();
            builder.RegisterComponentInHierarchy<EquipmentPresenter>();
            builder.RegisterComponentInHierarchy<PlayerPresenter>();

            builder.RegisterEntryPoint<EquipmentController>();
        }
    }
}