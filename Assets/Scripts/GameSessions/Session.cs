using System;
using Newtonsoft.Json;

[Serializable]
public readonly struct Session
{
    [JsonProperty("start")]
    public readonly DateTime Start;

    [JsonProperty("end")]
    public readonly DateTime End;

    [JsonIgnore]
    public readonly TimeSpan Duration => End - Start;

    [JsonIgnore]
    public readonly bool IsComplete => End != default && Start != default;

    public Session(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public Session(DateTime start)
    {
        Start = start;
        End = default;
    }
}