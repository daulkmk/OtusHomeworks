

using Lessons.Entities.Common.Components;
using Lessons.Level;
using UI;
using UnityEngine;
using VContainer;

namespace Lessons.Entities.Hero
{
    [RequireComponent(typeof(HeroEntity))]
    [RequireComponent(typeof(HeroView))]
    public class HeroInstaller : MonoBehaviour
    {
        [Inject]
        private void Construct(HeroesViewMap map)
        {
            var hero = GetComponent<HeroEntity>();
            var view = GetComponent<HeroView>();
            
            map.AddHero(hero, view);
        }
    } }