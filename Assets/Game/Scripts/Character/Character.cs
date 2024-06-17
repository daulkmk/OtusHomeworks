using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Lessons.Lesson_Components
{    
    //Facade
    [Is(ObjectType.Player, ObjectType.Damagable)]
    public class Character : AtomicEntity
    {
        [Get(CommonAPI.Position)]
        public IAtomicValueObservable<Vector3> Position => _position;

        [Get(MoveAPI.MoveDirection)]
        public IAtomicVariable<Vector3> MoveDirection => _characterCore.MoveComponent.MoveDirectionWorld;

        [Get(MoveAPI.LookTarget)]
        public IAtomicVariableObservable<Vector3> LookTarget => _lookTarget;
        

        [Get(ShootAPI.FirePointPosition)]
        public IAtomicValueObservable<Vector3> FirePointPosition => _characterCore.ShootComponent.FirePointPosition;

        [Get(ShootAPI.ShootRequest)] 
        public IAtomicAction ShootRequest => _characterCore.ShootComponent.ShootRequest;

        [Get(ShootAPI.Ammo)]
        public IAtomicVariableObservable<int> Ammo => _characterCore.AmmoComponent.Bullets;


        [Get(LifeAPI.TakeDamageAction)]
        public IAtomicAction<int> TakeDamageAction => _characterCore.LifeComponent.TakeDamageAction;

        [Get(LifeAPI.IsDead)]
        public IAtomicValueObservable<bool> IsDead => _characterCore.LifeComponent.IsDead;

        [Get(LifeAPI.Health)]
        public IAtomicValueObservable<int> Health => _characterCore.LifeComponent.HitPoints;
        
        //Секции
        [SerializeField] private CharacterCore _characterCore;
        
        //View
        [SerializeField] private CharacterAnimation _characterAnimation;
        [SerializeField] private CharacterVfx _vfx;
        [SerializeField] private CharacterAudio _audio;

        private readonly AtomicVariable<Vector3> _lookTarget = new();
        private readonly AtomicVariable<Vector3> _position = new();

        private void Awake()
        {
            _characterCore.Compose();
            _characterAnimation.Compose(_characterCore);
            _vfx.Compose(_characterCore);
            _audio.Compose(_characterCore);

            _lookTarget.Subscribe(x => _characterCore.LookAtTargetMechanics.TargetPoint.Value = x);
        }

        private void OnEnable()
        {
            _characterAnimation.OnEnable();
            _vfx.OnEnable();
            _audio.OnEnable();
        }

        private void OnDisable()
        {
            _characterAnimation.OnDisable();
            _vfx.OnDisable();
            _audio.OnDisable();
        }

        private void Update()
        {
            _characterCore.Update(Time.deltaTime);

            _position.Value = _characterCore.MoveComponent.Root.position;
        }
    }
}
