public class BotBlackboardFactory
{
    private readonly ConverterDock _converterDock;
    private readonly TreesManager _treesManager;
    private readonly BotsHiveMindData _hiveMindData;

    public BotBlackboardFactory(ConverterDock converterDock, TreesManager treesManager, BotsHiveMindData hiveMindData)
    {
        _converterDock = converterDock;
        _treesManager = treesManager;
        _hiveMindData = hiveMindData;
    }

    public BotBlackboard Create(Bot bot)
    {
        return new BotBlackboard(bot: bot, 
            converterDock: _converterDock, 
            treesManager: _treesManager,
            hiveMindData: _hiveMindData
        );
    }
}