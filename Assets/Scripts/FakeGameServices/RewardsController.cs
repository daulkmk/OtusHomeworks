using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RewardsController : IRewardsController, IDisposable
{
    private readonly CancellationTokenSource _cts = new();

    public UniTask GiveRewardToPlayer(RewardType rewardType, int amount)
    {
        Debug.Log($"GIVE REWARD: {rewardType} {amount}");
        
        return UniTask.WaitForSeconds(5, cancellationToken: _cts.Token);
    }

    void IDisposable.Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }
}