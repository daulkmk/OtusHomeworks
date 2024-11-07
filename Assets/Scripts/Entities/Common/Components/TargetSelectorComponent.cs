using Lessons.Game;
using Lessons.Game.Events.Effects;

namespace Lessons.Entities.Common.Components
{
    public sealed class TargetSelectorComponent
    {
        public ISelectTargetEffect Effect => _targetSelector.Effect;
        private readonly TargetSelector _targetSelector;

        public TargetSelectorComponent(TargetSelector targetSelector)
        {
            _targetSelector = targetSelector;
        }
    }
}