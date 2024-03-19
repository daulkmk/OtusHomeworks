using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class AICharacter : Character
    {
        [SerializeField] private AttackAgent _attackAgent;
        [SerializeField] private MoveAgent _moveAgent;

        public IAttackAgent AttackAgent => _attackAgent;
        public IMoveAgent MoveAgent => _moveAgent;

        protected override void Awake()
        {
            base.Awake();

            _attackAgent.Initialize(_weapon);
            _moveAgent.Initialize(_moveComponent);

            _attackAgent.CanAttackDelegate = CanAttackAgentAttack;
        }

        public void SetTargets(Vector2 moveTarget, Transform attackTarget)
        {
            _moveAgent.SetDestination(moveTarget);
            _attackAgent.SetTarget(attackTarget);
        }

        public void Reset()
        {
            _attackAgent.Reset();
            _hitPoints.Restore();
        }

        private bool CanAttackAgentAttack()
        {
            return _moveAgent.IsReached && base.CanFireWeapon();
        }
    }
}
