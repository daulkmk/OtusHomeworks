namespace Nodes.Bot
{
    public class FindDock : BotNode
    {
        public FindDock(BotBlackboard blackboard) : base(blackboard)
        {
        }

        public override NodeResult Update()
        {
            if (_blackboard.ConverterDock == null)
                return NodeResult.Fail;

            _blackboard.FollowTarget = _blackboard.ConverterDock.transform;
            return NodeResult.Success;
        }
    }
}