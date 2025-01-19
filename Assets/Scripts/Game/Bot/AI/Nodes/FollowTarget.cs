namespace Nodes.Bot
{
    public class FollowTarget : BotNode
    {
        public FollowTarget(BotBlackboard blackboard) : base(blackboard)
        {
        }

        public override NodeResult Update()
        {
            if (_blackboard.FollowTarget == null)
                return NodeResult.Fail;

            _blackboard.Bot.MoveToTargetComponent.Target = _blackboard.FollowTarget.position;

            if (_blackboard.Bot.MoveToTargetComponent.IsTargetReached())
                return NodeResult.Success;

            return NodeResult.Running;
        }
    }
}