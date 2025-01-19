using System.Linq;

namespace Nodes.Bot
{
    public class CollectTreeResource : BotNode
    {
        public CollectTreeResource(BotBlackboard blackboard) : base(blackboard)
        {
        }

        public override NodeResult Update()
        {
            var tree = _blackboard.OccupiedTree;
            var bot = _blackboard.Bot;

            if (tree == null)
                return NodeResult.Fail;

            if (!bot.TriggerComponent.EntitiesInRange.Contains(tree))
                return NodeResult.Fail;

            bool collected = bot.CollectResourceComponent.TryCollect(tree);

            return collected ? NodeResult.Success : NodeResult.Fail;
        }
    }
}