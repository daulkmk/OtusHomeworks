using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using VContainer.Unity;

public class GameSessionsManager : IGameSessions, IInitializable, IDisposable
{
    private const string SAVE_FILE_NAME = "sessions.json";

    private readonly ISeverTime _severTime;
    private readonly List<Session> _sessions = new();
    private readonly CancellationTokenSource _cts = new();
    private Session _activeSession;

    public bool IsReady { get; private set; }
    public IReadOnlyList<Session> Sessions => _sessions;
    public TimeSpan ActiveSessionDuration => _severTime.Now - _activeSession.Start;

    private string SaveFilePath => Path.Combine(Application.streamingAssetsPath, SAVE_FILE_NAME);

    public GameSessionsManager(ISeverTime severTime)
    {
        _severTime = severTime;
    }

    async void IInitializable.Initialize()
    {
        bool canceled = await UniTask.WaitUntil(() => _severTime.IsReady, cancellationToken: _cts.Token)
            .SuppressCancellationThrow();

        if (canceled)
            return;
            
        LoadSessions();
        StartSession();

        IsReady = true;
    }

    void IDisposable.Dispose()
    {
        if (IsReady)
        {
            EndSession();

            if (_activeSession.IsComplete)
                _sessions.Add(_activeSession);

            SaveSessions();
        }

        _cts.Cancel();
        _cts.Dispose();
    }

    private void StartSession()
    {
        _activeSession = new Session(
            start: _severTime.Now
        );
    }

    private void EndSession()
    {
        _activeSession = new Session(
            start: _activeSession.Start,
            end: _severTime.Now
        );
    }

    private void LoadSessions()
    {
        _sessions.Clear();

        string filePath = SaveFilePath;

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(SaveFilePath);
            var sessions = JsonConvert.DeserializeObject<List<Session>>(json);

            if (sessions != null)
                _sessions.AddRange(sessions);
        }
    }

    private void SaveSessions()
    {
        MakeShureSaveFileExists();

        string json = JsonConvert.SerializeObject(_sessions, Formatting.Indented);
        File.WriteAllText(SaveFilePath, json);
    }

    private void MakeShureSaveFileExists()
    {
        string filePath = SaveFilePath;

        string directory = Path.GetDirectoryName(filePath);

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();

#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }
    }
}
