using System;
using Nodes;
using Nodes.Bot;
using VContainer.Unity;

public class BotBrain : ITickable, IInitializable, IDisposable
{
    private readonly BotBlackboard _blackboard;
    private readonly Node _rootNode;

    public BotBrain(BotBlackboard blackboard)
    {
        _blackboard = blackboard;
        _rootNode = CreateBehaviourTree();
    }

    /// <returns>Root node</returns>
    private Node CreateBehaviourTree()
    {
        var followTarget = new FollowTarget(_blackboard); //Common node

        var gatherWoodSequence = CreateGatherWoodSequence();
        var deliverSequence = CreateDeliverSequence();

        var deliverOrGatherWood = new IsInventoryContainsWoodCondition(_blackboard, trueNode: deliverSequence, falseNode: gatherWoodSequence);

        var patrolSequnece = CreatePatrolSequence();

        return new Selector(deliverOrGatherWood, patrolSequnece);

        Node CreateGatherWoodSequence()
        {
            var findTree = new FindAvailableTree(_blackboard);
            var collectTree = new CollectTreeResource(_blackboard);

            return new Sequence(findTree, followTarget, collectTree);
        }

        Node CreateDeliverSequence()
        {
            var findDock = new FindDock(_blackboard);
            var unloadIntoDock = new UnloadWoodIntoDock(_blackboard);

            return new Sequence(findDock, followTarget, unloadIntoDock);
        }

        Node CreatePatrolSequence()
        {
            var findPoint = new FindPatrolPoint(_blackboard);
            return new Sequence(findPoint, followTarget);
        }
    }

    public void Initialize()
    {
        _rootNode.Initialize();
    }

    public void Dispose()
    {
        _rootNode.Dispose();
    }

    public void Tick()
    {
        _rootNode.Update();
    }
}