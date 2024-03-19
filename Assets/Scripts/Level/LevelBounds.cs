using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBounds : MonoBehaviour
    {
        [SerializeField] private Transform _leftBottomBorder;
        [SerializeField] private Transform _rightTopBorder;
        
        public bool InBounds(Vector3 position)
        {
            var x = position.x;
            var y = position.y;

            return x > _leftBottomBorder.position.x
                   && x < _rightTopBorder.position.x
                   && y > _leftBottomBorder.position.y
                   && y < _rightTopBorder.position.y;
        }
    }
}