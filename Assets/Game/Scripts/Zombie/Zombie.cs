using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace Lessons.Lesson_Components
{    
    //Facade
    [Is(ObjectType.Damagable)]
    public class Zombie : AtomicEntity
    {
        [Get(CommonAPI.Position)]
        public IAtomicValueObservable<Vector3> Position => _position;


        [Get(MoveAPI.MoveDirection)]
        public IAtomicVariable<Vector3> MoveDirection => _core.MoveComponent.MoveDirectionWorld;

        [Get(MoveAPI.LookTarget)]
        public IAtomicVariableObservable<Vector3> LookTarget => _lookTarget;
        

        [Get(PunchAPI.PunchRequest)]
        public IAtomicAction PunchRequest => _core.PunchComponent.AttackRequest;
        
        [Get(PunchAPI.IsTargetInRange)]
        public IAtomicValueObservable<bool> IsTargetInRange => _core.PunchComponent.IsTargetInRange;


        [Get(LifeAPI.TakeDamageAction)]
        public IAtomicAction<int> TakeDamageAction => _core.LifeComponent.TakeDamageAction;

        [Get(LifeAPI.IsDead)]
        public IAtomicValueObservable<bool> IsDead => _core.LifeComponent.IsDead;
        
        
        //Секции
        [SerializeField] private ZombieCore _core;
        
        //View
        [SerializeField] private ZombieAnimation _animation;
        [SerializeField] private ZombieVfx _vfx;
        [SerializeField] private ZombieAudio _audio;

        private AtomicVariable<Vector3> _lookTarget = new();
        private readonly AtomicVariable<Vector3> _position = new();


        private void Awake()
        {
            _core.Compose();
            _animation.Compose(_core);
            _vfx.Compose(_core);
            _audio.Compose(_core);

            _lookTarget.Subscribe(x => _core.LookAtTargetMechanics.TargetPoint.Value = x);
        }

        private void OnEnable()
        {
            _animation.OnEnable();
            _vfx.OnEnable();
            _audio.OnEnable();
        }

        private void OnDisable()
        {
            _animation.OnDisable();
            _vfx.OnDisable();
            _audio.OnDisable();
        }

        private void Update()
        {
            _core.Update(Time.deltaTime);

            _position.Value = _core.MoveComponent.Root.position;
        }
    }
}
