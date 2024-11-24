using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

public class SessionsViewController : IInitializable, IDisposable
{
    private readonly SessionsView _view;
    private readonly IGameSessions _sessions;

    private readonly CancellationTokenSource _cts = new();

    public SessionsViewController(SessionsView view, IGameSessions sessions)
    {
        _view = view;
        _sessions = sessions;
    }

    async void IInitializable.Initialize()
    {
        bool canceled = await UniTask.WaitUntil(() => _sessions.IsReady, cancellationToken: _cts.Token)
            .SuppressCancellationThrow();

        if (canceled)
            return;

        InitializeView();
        UpdateActiveSessionViewRoutine().Forget();
    }

    void IDisposable.Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    private void InitializeView()
    {
        var strings = new List<(string, string)>();
        foreach (var session in _sessions.Sessions)
        {
            strings.Add((session.Start.ToString(), session.End.ToString()));
        }

        _view.Initialize(strings);
    }

    private async UniTask UpdateActiveSessionViewRoutine()
    {
        while (!_cts.IsCancellationRequested)
        {
            UpdateActiveSessionView();
            await UniTask.WaitForSeconds(1, cancellationToken: _cts.Token);
        }
    }

    private void UpdateActiveSessionView()
    {
        string duration = _sessions.ActiveSessionDuration.ToString(@"dd\.hh\:mm\:ss");
        _view.UpdateActiveSession("Active session duration: " + duration);
    }
}
