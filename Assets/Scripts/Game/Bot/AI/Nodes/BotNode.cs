namespace Nodes.Bot
{
    public abstract class BotNode : Node
    {
        protected readonly BotBlackboard _blackboard;

        public BotNode(BotBlackboard blackboard)
        {
            _blackboard = blackboard;
        }
    }
}