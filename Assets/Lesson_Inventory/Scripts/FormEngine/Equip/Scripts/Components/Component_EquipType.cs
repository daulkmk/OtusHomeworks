using System;
using UnityEngine;

namespace Game.GameEngine.Mechanics
{
    [Serializable]
    public sealed class Component_EquipType
    {
        public EquipType Type
        {
            get { return this.type; }
        }

        [SerializeField]
        private EquipType type;
    }
}