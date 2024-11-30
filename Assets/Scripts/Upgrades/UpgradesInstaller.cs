using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class UpgradesFactory : MonoBehaviour
{
    private readonly IEnumerable<UpgradeConfig> _configs;
    private readonly IObjectResolver _objectResolver;

    private List<Upgrade> _upgrades;

    public UpgradesFactory(IEnumerable<UpgradeConfig> configs, IObjectResolver objectResolver)
    {
        _configs = configs;
        _objectResolver = objectResolver;
    }

    public IEnumerable<Upgrade> Create()
    {
        if (_upgrades == null)
        {
            _upgrades = new List<Upgrade>();
            foreach (var config in _configs)
            {
                var upgrade = config.Create();
                _objectResolver.Inject(upgrade);

                _upgrades.Add(upgrade);
            }
        }

        return _upgrades;
    }
}
