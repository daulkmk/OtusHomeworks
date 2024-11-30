using System.Collections.Generic;

public interface IUpgradesSystem
{
    IReadOnlyCollection<Upgrade> GetAllUpgrades();
    bool CanLevelUp(Upgrade upgrade);
    void LevelUp(Upgrade upgrade);
}