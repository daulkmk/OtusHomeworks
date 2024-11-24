using System;
using System.Collections.Generic;

public interface IGameSessions
{
    bool IsReady { get; }
    TimeSpan ActiveSessionDuration { get; }
    IReadOnlyList<Session> Sessions { get; }
}