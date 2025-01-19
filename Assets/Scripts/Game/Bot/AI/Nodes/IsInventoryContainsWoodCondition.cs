namespace Nodes.Bot
{
    public class IsInventoryContainsWoodCondition : Condition
    {
        private readonly BotBlackboard _blackboard;

        public IsInventoryContainsWoodCondition(BotBlackboard blackboard, Node trueNode, Node falseNode)
            : base(trueNode, falseNode)
        {
            _blackboard = blackboard;
        }

        protected override bool GetConditionValue()
        {
            return _blackboard.Bot.InventoryComponent.Contains(RESOURCE.WOOD);
        }
    }
}