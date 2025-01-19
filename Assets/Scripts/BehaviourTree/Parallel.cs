using System.Collections.Generic;
using System.Linq;

namespace Nodes
{
    public class Parallel : NodeWithChildren
    {
        protected readonly List<Node> _children = new();
        public override IEnumerable<Node> Children => _children;

        public override NodeResult Update()
        {
            bool isAllFailed = _children.All(x => x.Update() is NodeResult.Fail);
            return isAllFailed ? NodeResult.Fail : NodeResult.Success;
        }
    }
}