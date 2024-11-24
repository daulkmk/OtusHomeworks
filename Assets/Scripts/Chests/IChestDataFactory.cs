using System;

public interface IChestDataFactory
{
    ChestData Create(ChestTier chestTier, DateTime startDateTime);
}
