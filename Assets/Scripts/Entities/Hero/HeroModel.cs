using System.Collections;
using System.Collections.Generic;
using Declarative;
using Lessons.Entities.Common.Model;
using Lessons.Game;
using Lessons.Utils;
using UI;

namespace Lessons.Entities.Hero
{
    public class HeroModel : DeclarativeModel
    {
        [Section]
        public Position position;

        [Section]
        public Stats stats;

        [Section]
        public Life life;

        [Section]
        public Weapon weapon;

        [Section]
        public Shield shield;

        [Section]
        public DamageHandler damageHandler;

        [Section]
        public TargetSelector targetSelector;

        [Section]
        public SFX sfx;

        public AtomicVariable<HeroView> view;
    }
}