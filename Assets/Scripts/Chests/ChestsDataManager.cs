using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using VContainer.Unity;


public class ChestsDataManager : IChestsDataManager, IInitializable, IDisposable
{
    private readonly static string s_pathToChestsData = Path.Combine(Application.streamingAssetsPath, "chests.json");

    private readonly CancellationTokenSource _cts = new();
    private readonly ISeverTime _serverTime;
    private readonly IChestDataFactory _chestsDataFactory;

    private List<ChestData> _chestsData;

    public bool IsReady { get; private set; }
    public IReadOnlyCollection<ChestData> ChestDatas => _chestsData;

    public ChestsDataManager(ISeverTime serverTime, IChestDataFactory chestDataFactory)
    {
        _serverTime = serverTime;
        _chestsDataFactory = chestDataFactory;
    }

    async void IInitializable.Initialize()
    {
        bool canceled = await UniTask.WaitUntil(() => _serverTime.IsReady, cancellationToken: _cts.Token)
            .SuppressCancellationThrow();

        if (canceled)
            return;

        await CreateChestsDataForTheFirstTime();
        await LoadChestsData();
        IsReady = true;
    }

    void IDisposable.Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    public bool TryToFindChestData(ChestTier chestTier, out ChestData chestData)
    {
        foreach (var data in _chestsData)
        {
            if (data.ChestTier == chestTier)
            {
                chestData = data;
                return true;
            }
        }

        chestData = default;
        return false;
    }

    public ChestData CreateNewChest(ChestTier chestTier)
    {
        if (!IsReady)
            throw new Exception("ChestsDataManager is not ready!");

        int _chestIndex = _chestsData.FindIndex(x => x.ChestTier == chestTier);
        if (_chestIndex == -1)
            throw new Exception("Chest tier is not supported! " + chestTier.ToString());

        _chestsData[_chestIndex] = _chestsDataFactory.Create(chestTier, _serverTime.Now);;

        SaveChestsData(_chestsData).Forget();

        return _chestsData[_chestIndex];
    }

    private async UniTask CreateChestsDataForTheFirstTime()
    {
        if (File.Exists(s_pathToChestsData))
            return;

        var dateTimeNow = _serverTime.Now;

        var data = new List<ChestData>
        {
            _chestsDataFactory.Create(ChestTier.Wooden, dateTimeNow),
            _chestsDataFactory.Create(ChestTier.Steel, dateTimeNow),
            _chestsDataFactory.Create(ChestTier.Gold, dateTimeNow)
        };

        await SaveChestsData(data);

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }

    private async UniTask SaveChestsData(List<ChestData> chestsData)
    {
        Debug.Log("Save chests data " + s_pathToChestsData);

        string json = JsonConvert.SerializeObject(chestsData, Formatting.Indented);
        await File.WriteAllTextAsync(s_pathToChestsData, json, cancellationToken: _cts.Token);
    }

    private async UniTask LoadChestsData()
    {      
        string json = await File.ReadAllTextAsync(s_pathToChestsData, cancellationToken: _cts.Token);

        try
        {
            _chestsData = JsonConvert.DeserializeObject<List<ChestData>>(json);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            Debug.Log("Failed to parse chests data! data: " + json);
            return;
        }
    }
}