using System.Collections;
using System.Collections.Generic;
using Entities;
using Lessons.Entities.Common.Components;
using UnityEngine;

namespace Lessons.Entities.Hero
{
    [RequireComponent(typeof(HeroModel))]
    [DefaultExecutionOrder(-100)]
    public class HeroEntity : MonoEntityBase
    {
        private void Awake()
        {
            HeroModel model = GetComponent<HeroModel>();
            Add(new PositionComponent(model.position.transform));
            Add(new CoordinatesComponent(model.position.coordinates));
            Add(new HitPointsComponent(model.life.hitPoints, model.life.maxHitPoints));
            Add(new DeathComponent(model.life.isDead));
            Add(new DestroyComponent(gameObject));
            Add(new TransformComponent(model.position.transform));
        }
    }
}