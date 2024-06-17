using System;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.DI
{
    public sealed class CameraFollow : ILateTickable
    {
        private readonly Camera _camera;
        private readonly Transform _target;
        
        private Vector3 _offset;

        public CameraFollow(Camera camera, Transform character)
        {
            _camera = camera;
            _target = character;

            var ray = new Ray(character.position, -camera.transform.forward);
            var cameraPositiom = RayPlaneIntersection.CalculateIntersection(_camera.transform.position.y, ray);

            _camera.transform.position = cameraPositiom;

            _offset = cameraPositiom - _target.position;
        }

        void ILateTickable.LateTick()
        {
            var cameraPosition = _target.position + _offset;
            _camera.transform.position = cameraPosition;
        }
    }
}