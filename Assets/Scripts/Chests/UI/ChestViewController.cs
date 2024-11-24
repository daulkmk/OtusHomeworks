
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

public class ChestViewController : IInitializable, IDisposable
{
    private readonly ChestView _view;
    private readonly ChestTier _chestTier;
    private readonly IRewardsController _rewardsController;
    private readonly IChestsDataManager _chestsDataManager;
    private readonly CancellationTokenSource _cts = new();

    private CancellationTokenSource _ctsUpdateRoutine;
    private ChestData? _chestData;

    public ChestViewController(ChestView view, ChestTier chestTier, IRewardsController rewardsController, IChestsDataManager chestsDataManager)
    {
        _view = view;
        _chestTier = chestTier;
        _rewardsController = rewardsController;
        _chestsDataManager = chestsDataManager;
    }

    async void IInitializable.Initialize()
    {
        _view.Close();
        _view.SetIsReadyForOpen(false);

        await SetChestData();
        
        _view.OnOpenButtonClick += HandleOpenButtonClick;

        StartUpdateViewStatusRoutine();
    }

    void IDisposable.Dispose()
    {
        _view.OnOpenButtonClick -= HandleOpenButtonClick;

        if (_ctsUpdateRoutine != null)
        {
            _ctsUpdateRoutine.Cancel();
            _ctsUpdateRoutine.Dispose();
        }

        _cts.Cancel();
        _cts.Dispose();
    }

    private async UniTask SetChestData()
    {
        await UniTask.WaitUntil(() => _chestsDataManager.IsReady, cancellationToken: _cts.Token);

        if (_chestsDataManager.TryToFindChestData(_chestTier, out var data))
        {
            _chestData = data;
        }
        else
        {
            _view.gameObject.SetActive(false);
            throw new Exception("There is no data for chest: " + _chestTier);
        }
    }

    private void StartUpdateViewStatusRoutine()
    {
        _ctsUpdateRoutine?.Dispose();

        _ctsUpdateRoutine = new CancellationTokenSource();
        UpdateViewStatusRoutine(_ctsUpdateRoutine.Token).Forget();
    }

    private async void HandleOpenButtonClick()
    {
        _ctsUpdateRoutine.Cancel();

        _view.Open();
        _view.SetIsReadyForOpen(false);
        _view.SetStatus("Some reward animation is here =D");

        //Create next chest immediatly - before animation ended
        var nextChest = _chestsDataManager.CreateNewChest(_chestTier);

        await GiveReward();
        
        Debug.Log("! SHOW NEXT CHEST AUTOMATICALLY !");

        _view.Close();
        _chestData = nextChest;

        StartUpdateViewStatusRoutine();
    }

    private UniTask GiveReward()
    {
        var rewardType = _chestData.Value.RewardType;
        var reward = _chestData.Value.Reward;

        return _rewardsController.GiveRewardToPlayer(rewardType, reward);
    }

    private async UniTask UpdateViewStatusRoutine(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            UpdateViewStatus();
            
            bool canceled = await UniTask.WaitForSeconds(1, cancellationToken: cancellationToken)
                .SuppressCancellationThrow();

            if (canceled)
                break;
        }
    }

    private void UpdateViewStatus()
    {
        DateTime now = DateTime.Now;
        DateTime canOpenTime = _chestData.Value.CanOpenTime;

        bool isReadyForOpen = now > canOpenTime;
        _view.SetIsReadyForOpen(isReadyForOpen);

        string status = isReadyForOpen ? "Open me!" : (canOpenTime - now).ToString(@"hh\:mm\:ss");
        _view.SetStatus(status);
    }
}