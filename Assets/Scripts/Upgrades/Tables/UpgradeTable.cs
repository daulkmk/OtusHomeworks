using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Meta
{
    [Serializable]
    public sealed class UpgradeTableFloat
    {
        public float ValueStep
        {
            get { return this.valueStep; }
        }

        [Space]
        [InfoBox("Linear Function")]
        [SerializeField]
        private float startValue;

        [SerializeField]
        private float endValue;

        [ReadOnly]
        [SerializeField]
        private float valueStep;

        [Space]
        [ReadOnlyArray]
        [ListDrawerSettings(
            IsReadOnly = true,
            OnBeginListElementGUI = "DrawLabelForListElement"
        )]
        [SerializeField]
        private float[] table;

        public float GetValue(int level)
        {
            var index = level - 1;
            return this.table[index];
        }

        public void OnValidate(int maxLevel)
        {
            this.EvaluateTable(maxLevel);
        }

        private void EvaluateTable(int maxLevel)
        {
            this.table = new float[maxLevel];
            this.table[0] = this.startValue;
            this.table[maxLevel - 1] = this.endValue;

            var step = (this.endValue - this.startValue) / (maxLevel - 1);
            this.valueStep = (float) Math.Round(step, 2);

            for (var i = 1; i < maxLevel - 1; i++)
            {
                var speed = this.startValue + this.valueStep * i;
                this.table[i] = (float) Math.Round(speed, 2);
            }
        }

#if UNITY_EDITOR
        private void DrawLabelForListElement(int index)
        {
            GUILayout.Space(8);
            GUILayout.Label($"Level {index + 1}");
        }
#endif
    }
}