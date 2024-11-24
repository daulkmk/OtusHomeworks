using System;

public interface ISeverTime
{
    bool IsReady { get; }
    DateTime Now { get; }
}