using System.Collections.Generic;

namespace Nodes
{
    public class Sequence : NodeWithChildren
    {
        protected readonly List<Node> _children = new();
        public override IEnumerable<Node> Children => _children;

        public Sequence(params Node[] children)
        {
            _children.AddRange(children);
        }

        public override NodeResult Update()
        {
            foreach (var child in _children)
            {
                var nodeResult = child.Update();

                if (nodeResult is NodeResult.Fail)
                    return NodeResult.Fail;

                if (nodeResult is NodeResult.Running)
                    return NodeResult.Running;

            }
            return NodeResult.Success;
        }
    }
}