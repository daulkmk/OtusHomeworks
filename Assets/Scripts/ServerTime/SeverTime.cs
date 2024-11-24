using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using VContainer.Unity;

public partial class SeverTime : ISeverTime, IInitializable, IDisposable
{
    private const string URL_GET_DATE = "https://worldtimeapi.org/api/timezone/Etc/UTC";

    private DateTime _serverBaseTime;
    private DateTime _localBaseTime;

    private readonly CancellationTokenSource _cts = new();

    public bool IsReady { get; private set; }

    public DateTime Now => DateTime.Now + (_serverBaseTime - _localBaseTime);

    private bool IsConnectedToInternet => Application.internetReachability != NetworkReachability.NotReachable;

    async void IInitializable.Initialize()
    {       
        await RequestTimeFromServer();

        IsReady = true;
    }

    void IDisposable.Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    private async UniTask RequestTimeFromServer()
    {
        if (!IsConnectedToInternet)
        {
            Debug.LogError("Please connect to the internet!");
            await UniTask.WaitUntil(() => IsConnectedToInternet, cancellationToken: _cts.Token);
        }
        
        using var request = UnityWebRequest.Get(URL_GET_DATE);
        
        var (canceled, _) = await request.SendWebRequest()
            .ToUniTask(cancellationToken: _cts.Token)
            .SuppressCancellationThrow();

        if (canceled)
            return;

        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;
            HandleServerResponse(response);
        }
        else
        {
            throw new Exception($"Failed to request time. Result: {request.result} Error: {request.error}");
        }
    }

    private void HandleServerResponse(string response)
    {
        ServerResponse srvResponse;
        try
        {
            srvResponse = JsonConvert.DeserializeObject<ServerResponse>(response);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            Debug.Log("Failed to parse server response! response: " + response);
            return;
        }

        if (!DateTime.TryParse(srvResponse.DateTimeUTC, out DateTime serverTimeUTC))
        {
            Debug.Log("Failed to parse date from response! date: " + srvResponse.DateTimeUTC);
            return;
        }

        _localBaseTime = DateTime.Now;
        _serverBaseTime = serverTimeUTC.ToLocalTime();

        Debug.Log(_serverBaseTime);
    }
}
