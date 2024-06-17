using UnityEngine;
using Atomic.Elements;

namespace Lessons.Lesson_AtomicIntroduction
{
    public class GetInputTargetMechanics
    {
        public IAtomicValueObservable<Vector3> Target => _target;

        private readonly AtomicVariable<Vector3> _target = new();
        private readonly Camera _camera;
        private readonly IAtomicValue<float> _yPosition;

        public GetInputTargetMechanics(Camera camera, IAtomicValue<float> yPosition)
        {
            _camera = camera;
            _yPosition = yPosition;
        }

        public void Update()
        {
            var mousePosition = Input.mousePosition;
            var ray = _camera.ScreenPointToRay(mousePosition);

            var position = RayPlaneIntersection.CalculateIntersection(_yPosition.Value, ray);
            
            _target.Value = position;
        }
    }
}