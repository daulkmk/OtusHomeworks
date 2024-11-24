using System;
using System.Collections.Generic;

using UniRandom = UnityEngine.Random;

public class ChestDataFactory : IChestDataFactory
{
    private readonly IReadOnlyList<RewardType> _allRewardTypes;

    public ChestDataFactory()
    {
        _allRewardTypes = (RewardType[])Enum.GetValues(typeof(RewardType));
    }

    public ChestData Create(ChestTier chestTier, DateTime startDateTime)
    {
        var rewardType = _allRewardTypes[UniRandom.Range(0, _allRewardTypes.Count)];

        return new ChestData
        (
            startDateTime: startDateTime,
            timeToOpen: GetTimeToOpen(chestTier),
            reward: GetRewardSize(rewardType, chestTier),
            rewardType: rewardType,
            chestTier: chestTier
        );
    }

    private int GetRewardSize(RewardType rewardType, ChestTier chestTier)
    {
        return rewardType switch
        {
            RewardType.Gold => chestTier switch
            {
                ChestTier.Wooden => 5,
                ChestTier.Steel => 10,
                ChestTier.Gold => 50,
                _ => throw new NotImplementedException($"{rewardType}-{chestTier}")
            },
            RewardType.Resources => chestTier switch
            {
                ChestTier.Wooden => 50,
                ChestTier.Steel => 100,
                ChestTier.Gold => 500,
                _ => throw new NotImplementedException($"{rewardType}-{chestTier}")
            },
            RewardType.SmthElse => chestTier switch
            {
                ChestTier.Wooden => 1,
                ChestTier.Steel => 2,
                ChestTier.Gold => 5,
                _ => throw new NotImplementedException($"{rewardType}-{chestTier}")
            },
            _ => throw new NotImplementedException(rewardType.ToString())
        };
    }

    private TimeSpan GetTimeToOpen(ChestTier chestTier)
    {
        return chestTier switch
        {
            ChestTier.Wooden => TimeSpan.FromSeconds(15),
            ChestTier.Steel => TimeSpan.FromMinutes(1),
            ChestTier.Gold => TimeSpan.FromHours(2),
            _ => throw new NotImplementedException(chestTier.ToString())
        };
    }
}