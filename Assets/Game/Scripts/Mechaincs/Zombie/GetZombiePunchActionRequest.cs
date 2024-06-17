using UnityEngine;
using Atomic.Elements;

namespace Lessons.Lesson_AtomicIntroduction
{
    public class GetZombiePunchActionRequest
    {
        public IAtomicEvent OnPunchRequested => _punchRequested;

        private readonly AtomicEvent _punchRequested = new();
        private readonly IAtomicValueObservable<bool> _isTargetInRange;
        private readonly IAtomicValueObservable<bool> _isTargetDead;

        public GetZombiePunchActionRequest(IAtomicValueObservable<bool> isTargetInRange,
            IAtomicValueObservable<bool> isTargetDead)
        {
            _isTargetInRange = isTargetInRange;
            _isTargetDead = isTargetDead;
        }

        public void Update()
        {
            if (_isTargetInRange.Value && !_isTargetDead.Value)
                _punchRequested.Invoke();
        }
    }
}