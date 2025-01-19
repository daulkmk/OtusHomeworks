using System.Linq;
using Entities;
using Sirenix.OdinInspector.Editor.Validation;

namespace Nodes.Bot
{
    public class FindAvailableTree : BotNode
    {
        public FindAvailableTree(BotBlackboard blackboard) : base(blackboard)
        {
        }

        public override NodeResult Update()
        {
            if (_blackboard.OccupiedTree != null)
                return NodeResult.Success;

            var availableTree = _blackboard.TreesManager.Trees.FirstOrDefault(
                t => !_blackboard.HiveMindData.OccupiedTrees.Contains(t)
            );

            if (availableTree == null)
                return NodeResult.Fail;

            _blackboard.FollowTarget = availableTree.transform;
            _blackboard.OccupiedTree = availableTree;
            _blackboard.HiveMindData.OccupiedTrees.Add(availableTree);

            availableTree.LifeComponent.OnDeath += OnTreeDestroyed;

            return NodeResult.Success;
        }

        private void OnTreeDestroyed(IEntity entity)
        {
            var tree = entity as Tree;
            UnsubscribeFromTree(tree);   
        }

        public override void Dispose()
        {
            base.Dispose();

            if (_blackboard.OccupiedTree != null)
                UnsubscribeFromTree(_blackboard.OccupiedTree);
        }

        private void UnsubscribeFromTree(Tree tree)
        {
            tree.LifeComponent.OnDeath -= OnTreeDestroyed;
            _blackboard.HiveMindData.OccupiedTrees.Remove(tree);

            _blackboard.OccupiedTree = null;

            if (_blackboard.FollowTarget == tree.transform)
                _blackboard.FollowTarget = null;
        }
    }
}