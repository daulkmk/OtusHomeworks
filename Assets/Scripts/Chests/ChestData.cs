using System;
using Newtonsoft.Json;

[Serializable]
public readonly struct ChestData
{
    [JsonProperty("start_date_time")]
    public readonly DateTime StartDateTime;

    [JsonProperty("time_to_open")]
    public readonly TimeSpan TimeToOpen;

    [JsonProperty("reward")]
    public readonly int Reward;

    [JsonProperty("reward_type")]
    public readonly RewardType RewardType;

    [JsonProperty("chest_tier")]
    public readonly ChestTier ChestTier;

    [JsonIgnore]
    public readonly DateTime CanOpenTime => StartDateTime + TimeToOpen;

    public ChestData(DateTime startDateTime, TimeSpan timeToOpen, int reward, RewardType rewardType, ChestTier chestTier)
    {
        StartDateTime = startDateTime;
        TimeToOpen = timeToOpen;
        Reward = reward;
        RewardType = rewardType;
        ChestTier = chestTier;
    }
}