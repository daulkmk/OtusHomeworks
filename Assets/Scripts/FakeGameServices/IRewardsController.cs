using Cysharp.Threading.Tasks;

public interface IRewardsController
{
    UniTask GiveRewardToPlayer(RewardType rewardType, int amount);
}
