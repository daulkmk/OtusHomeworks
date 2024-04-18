using UnityEngine;

namespace ShootEmUp
{
    public interface ITransform
    {
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
    }

    public sealed class TransformInfo : ITransform
    {
        Vector3 ITransform.Position
        {
            get => _transform.position;
            set => _transform.position = value;
        }

        Quaternion ITransform.Rotation
        {
            get => _transform.rotation;
            set => _transform.rotation = value;
        }

        private readonly Transform _transform;

        public TransformInfo(Transform transform)
        {
            _transform = transform;
        }
    }
}