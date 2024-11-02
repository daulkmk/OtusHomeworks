using System.Collections;
using System.Collections.Generic;
using Declarative;
using Lessons.Entities.Common.Model;
using UI;

namespace Lessons.Entities.Hero
{
    public class HeroModel : DeclarativeModel
    {
        [Section]
        public Position position;

        [Section]
        public Life life;

        public HeroView view;
    }
}