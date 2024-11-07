using Cysharp.Threading.Tasks;
using UI;

namespace Lessons.Game.Turn.Visual.Tasks
{
    public sealed class AttackVisualTask : Task
    {
        private readonly HeroView _attacker;
        private readonly HeroView _target;

        public AttackVisualTask(HeroView attacker, HeroView target)
        {
            _attacker = attacker;
            _target = target;
        }

        protected override UniTask OnRun()
        {
            return _attacker.AnimateAttack(_target);
        }
    }
}