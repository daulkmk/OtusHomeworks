using UnityEngine;
using VContainer.Unity;

public class MoveComponent : ITickable
{
    private readonly Transform _transform;

    public Vector3 Direction { get; set; }
    public float Speed { get; set; } = 1;

    public Vector3 Position => _transform.position;

    public MoveComponent(Transform transform)
    {
        _transform = transform;
    }

    void ITickable.Tick()
    {
        if (Direction != Vector3.zero)
        {
            var translation = Time.deltaTime * Speed * Direction.normalized;
            _transform.position += translation;
        }
    }
}
