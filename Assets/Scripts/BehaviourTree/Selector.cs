using System.Collections.Generic;
using System.Linq;

namespace Nodes
{
    public class Selector : NodeWithChildren
    {
        protected readonly List<Node> _children = new();
        public override IEnumerable<Node> Children => _children;
        
        public Selector(params Node[] children)
        {
            _children.AddRange(children);
        }
        
        public override NodeResult Update()
        {
            var node = _children.FirstOrDefault(x => x.Update() is NodeResult.Success or NodeResult.Running);
            return node == null ? NodeResult.Fail : NodeResult.Success;
        }
    }
}