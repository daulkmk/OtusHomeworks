using System.Collections;
using System.Collections.Generic;
using Atomic.Elements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Lessons.Lesson_AtomicIntroduction
{
    public class GetPlayerShootActionRequest
    {
        public IAtomicEvent ShootRequested => _shootRequested;
        public AtomicEvent _shootRequested = new();

        public void Update()
        {
            if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
                _shootRequested?.Invoke();
        }
    }
}