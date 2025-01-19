using System;
using VContainer.Unity;

namespace Nodes
{
    public abstract class Node : IInitializable, IDisposable
    {
        public virtual void Initialize() { }
        public virtual void Dispose() { }

        public abstract NodeResult Update();
    }
}