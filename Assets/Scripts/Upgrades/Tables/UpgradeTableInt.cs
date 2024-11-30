using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Meta
{
    [Serializable]
    public sealed class UpgradeTableInt
    {
        [Space]
        [SerializeField]
        private int baseValue;

        [Space]
        [ListDrawerSettings(OnBeginListElementGUI = "DrawLevels")]
        [ReadOnlyArray]
        [SerializeField]
        private int[] levels;

        public int GetValue(int level)
        {
            var index = level - 1;
            index = Mathf.Clamp(index, 0, this.levels.Length - 1);
            return this.levels[index];
        }

        private void DrawLevels(int index)
        {
            GUILayout.Space(8);
            GUILayout.Label($"Level #{index + 1}");
        }
        
        public void OnValidate(int maxLevel)
        {
            this.EvaluateTable(maxLevel);
        }

        private void EvaluateTable(int maxLevel)
        {
            var table = new int[maxLevel];
            table[0] = baseValue;
            for (var level = 2; level <= maxLevel; level++)
            {
                var value = this.baseValue * level;
                table[level - 1] = value;
            }

            this.levels = table;
        }
    }
}