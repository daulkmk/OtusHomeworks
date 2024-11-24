using System.Collections.Generic;

public interface IChestsDataManager
{
    bool IsReady { get; }
    IReadOnlyCollection<ChestData> ChestDatas { get; }

    bool TryToFindChestData(ChestTier chestTier, out ChestData chestData);
    ChestData CreateNewChest(ChestTier chestTier);
}
