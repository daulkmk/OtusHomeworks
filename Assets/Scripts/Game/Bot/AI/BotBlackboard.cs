using UnityEngine;

public class BotBlackboard
{
    //Static
    public Bot Bot { get; private set; }
    public ConverterDock ConverterDock { get; private set; }
    public TreesManager TreesManager { get; private set; }
    public BotsHiveMindData HiveMindData { get; private set; }

    //Variables
    public Tree OccupiedTree { get; set; }
    public Transform FollowTarget { get; set; }

    public BotBlackboard(Bot bot,
        ConverterDock converterDock,
        TreesManager treesManager,
        BotsHiveMindData hiveMindData
    )
    {
        Bot = bot;
        ConverterDock = converterDock;
        TreesManager = treesManager;
        HiveMindData = hiveMindData;
    }
}