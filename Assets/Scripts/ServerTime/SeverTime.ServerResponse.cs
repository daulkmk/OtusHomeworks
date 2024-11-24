using Newtonsoft.Json;

public partial class SeverTime
{
    private struct ServerResponse
    {
        [JsonProperty("datetime")]
        public readonly string DateTimeUTC;
    }
}
