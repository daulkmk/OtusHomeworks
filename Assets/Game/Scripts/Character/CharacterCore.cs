using System;
using Atomic.Elements;
using Lessons.Lesson_AtomicIntroduction;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    [Serializable]
    public class CharacterCore
    {
        public MoveComponent MoveComponent;
        public LifeComponent LifeComponent;
        public RotationComponent RotationComponent;
        public ShootComponent ShootComponent;
        public AmmoComponent AmmoComponent;

        public LookAtTargetMechanics LookAtTargetMechanics;
        public ReplanishAmmoMechanics ReplanishAmmoMechanics;
        public ConsumeAmmoMechanics ConsumeAmmoMechanics;

        [SerializeField] private Collider _collider;


        public void Compose()
        {
            LifeComponent.Compose();

            MoveComponent.Compose();
            MoveComponent.AppendCondition(LifeComponent.IsAlive);

            RotationComponent.Construct();
            RotationComponent.AppendCondition(LifeComponent.IsAlive);

            ShootComponent.Compose();
            ShootComponent.AppendCondition(LifeComponent.IsAlive);
            ShootComponent.AppendCondition(AmmoComponent.HasBullets);

            var rootPosition = new AtomicFunction<Vector3>(() =>
            {
                return RotationComponent.RotationRoot.position;
            });

            LookAtTargetMechanics =
                new LookAtTargetMechanics(rotateAction: RotationComponent.RotateAction, transform: rootPosition);
            LookAtTargetMechanics.AppendCondition(LifeComponent.IsAlive);

            ReplanishAmmoMechanics =
                new ReplanishAmmoMechanics(interval: 2, ammoAmount: 1, AmmoComponent.Bullets);

            ConsumeAmmoMechanics =
                new ConsumeAmmoMechanics(ShootComponent.ShootEvent, AmmoComponent.Bullets);

            LifeComponent.IsDead.Subscribe(isDead => _collider.enabled = !isDead);
        }

        public void Update(float deltaTime)
        {
            ReplanishAmmoMechanics.Update(deltaTime);

            MoveComponent.Update(deltaTime);
            ShootComponent.Update(deltaTime);
            
            LookAtTargetMechanics.Update();
        }
    }
}