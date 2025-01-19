namespace Nodes.Bot
{
    public class FindPatrolPoint : BotNode
    {
        public FindPatrolPoint(BotBlackboard blackboard) : base(blackboard)
        {
        }

        public override NodeResult Update()
        {
            var bot = _blackboard.Bot;

            if (bot.PatrolPointsComponent.CurrentPoint == null)
                return NodeResult.Fail;

            if (_blackboard.FollowTarget == bot.PatrolPointsComponent.CurrentPoint && bot.MoveToTargetComponent.IsTargetReached())
            {
                bot.PatrolPointsComponent.NextPoint();
            }
            _blackboard.FollowTarget = bot.PatrolPointsComponent.CurrentPoint;

            return NodeResult.Success;
        }
    }
}