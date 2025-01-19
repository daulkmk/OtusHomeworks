using System.Collections.Generic;
using Sirenix.Utilities;

namespace Nodes
{
    public abstract class NodeWithChildren : Node
    {
        public abstract IEnumerable<Node> Children { get; }

        public override void Initialize()
        {
            base.Initialize();
            Children.ForEach(c => c.Initialize());
        }

        public override void Dispose()
        {
            base.Dispose();
            Children.ForEach(c => c.Dispose());
        }
    }
}