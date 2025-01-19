using System.Collections.Generic;

namespace Nodes
{
    public abstract class Condition : NodeWithChildren
    {
        private readonly Node[] _nodes = new Node[2];
        public override IEnumerable<Node> Children => _nodes;

        public Condition(Node trueNode, Node falseNode)
        {
            _nodes[0] = trueNode;
            _nodes[1] = falseNode;
        }

        public override NodeResult Update()
        {
            var node = GetConditionValue() ? _nodes[0] : _nodes[1];
            return node.Update();
        }

        protected abstract bool GetConditionValue();
    }
}