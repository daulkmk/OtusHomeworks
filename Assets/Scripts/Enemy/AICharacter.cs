using UnityEngine;
using R3;

namespace ShootEmUp
{
    public class AICharacter : Character
    {
        public IAttackAgent AttackAgent { get; private set; }
        public IMoveAgent MoveAgent { get; private set; }

        private CompositeDisposable _disposables;

        public AICharacter(IAttackAgent attackAgent, IMoveAgent moveAgent, IWeapon weapon, IMove move, IHitPoints hitPoints, IGameObject gameObject)
            : base(weapon, move, hitPoints, gameObject, false)
        {
            AttackAgent = attackAgent;
            MoveAgent = moveAgent;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _disposables = new CompositeDisposable(
                Weapon.CanFire.Subscribe(OnAttackAgentConditionChanged),
                MoveAgent.IsReached.Subscribe(OnAttackAgentConditionChanged)
            );

            UpdateAttackAgent();
        }

        protected override void Dispose()
        {
            _disposables.Dispose();
            base.Dispose();
        }

        public void SetTargets(Vector2 moveTarget, ITransform attackTarget)
        {
            MoveAgent.SetDestination(moveTarget);
            AttackAgent.SetTarget(attackTarget);
        }

        public void Reset()
        {
            AttackAgent.Reset();
            HitPoints.Restore();
        }

        private void OnAttackAgentConditionChanged(bool _)
        {
            UpdateAttackAgent();
        }

        private bool CanAttackAgentAttack()
        {
            return MoveAgent.IsReached.CurrentValue && base.CanFireWeapon();
        }

        private void UpdateAttackAgent()
        {
            AttackAgent.CanAttack.Value = CanAttackAgentAttack();
        }
    }
}
