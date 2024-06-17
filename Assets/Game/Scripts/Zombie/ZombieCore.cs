using System;
using Atomic.Elements;
using Lessons.Lesson_AtomicIntroduction;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    [Serializable]
    public class ZombieCore
    {
        public MoveComponent MoveComponent;
        public LifeComponent LifeComponent;
        public RotationComponent RotationComponent;
        public PunchComponent PunchComponent;
        public LookAtTargetMechanics LookAtTargetMechanics;

        [SerializeField] private Collider _collider;


        public void Compose()
        {
            LifeComponent.Compose();

            MoveComponent.Compose();
            MoveComponent.AppendCondition(LifeComponent.IsAlive);
            
            RotationComponent.Construct();
            RotationComponent.AppendCondition(LifeComponent.IsAlive);
            
            PunchComponent.Construct();
            PunchComponent.AppendCondition(LifeComponent.IsAlive);

            var rootPosition = new AtomicFunction<Vector3>(() =>
            {
                return RotationComponent.RotationRoot.position;
            });

            LookAtTargetMechanics =
                new LookAtTargetMechanics(rotateAction: RotationComponent.RotateAction, transform: rootPosition);
            LookAtTargetMechanics.AppendCondition(LifeComponent.IsAlive);

            LifeComponent.IsDead.Subscribe(isDead => _collider.enabled = !isDead);
        }

        public void Update(float deltaTime)
        {
            MoveComponent.Update(deltaTime);
            PunchComponent.Update(deltaTime);

            LookAtTargetMechanics.Update();
        }
    }
}