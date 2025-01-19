using System.Linq;

namespace Nodes.Bot
{
    public class UnloadWoodIntoDock : BotNode
    {
        public UnloadWoodIntoDock(BotBlackboard blackboard) : base(blackboard)
        {
        }

        public override NodeResult Update()
        {
            var bot = _blackboard.Bot;

            if (!bot.InventoryComponent.Contains(RESOURCE.WOOD))
                return NodeResult.Fail;

            var dock = _blackboard.ConverterDock;
            if (!bot.TriggerComponent.EntitiesInRange.Contains(dock))
                return NodeResult.Fail;

            if (!dock.CanLoadResource(RESOURCE.WOOD))
                return NodeResult.Running;

            dock.LoadResource(RESOURCE.WOOD);
            bot.InventoryComponent.Remove(RESOURCE.WOOD);
            
            return NodeResult.Success;
        }
    }
}