using System;

namespace Client.Components
{
    [Serializable]
    public struct Collision
    {
        public int Entity;

        public Collision(int entity) { Entity = entity; }
    }
}
