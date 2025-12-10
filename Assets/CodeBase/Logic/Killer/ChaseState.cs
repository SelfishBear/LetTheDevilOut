using UnityEngine;

namespace CodeBase.Logic.Killer
{
    public class ChaseState : IEnemyState
    {
        private readonly EnemyAI _enemyAI;
        private readonly EnemyStateMachine _enemyStateMachine;

        public ChaseState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine)
        {
            _enemyAI = enemyAI;
            _enemyStateMachine = enemyStateMachine;
        }
        public void Enter()
        {
            _enemyAI.NavMeshAgent.speed = _enemyAI.RunSpeed;
            Debug.Log("Enter Chase State");
        }

        public void Update()
        {
            _enemyAI.NavMeshAgent.SetDestination(_enemyAI.TargetPlayer.transform.position);

            if (_enemyAI.CanAttackPlayer())
            {
                _enemyStateMachine.ChangeState<AttackState>(new AttackState(_enemyAI, _enemyStateMachine));
            }

            if (!_enemyAI.CanSeePlayer())
            {
                _enemyStateMachine.ChangeState<PatrolState>(new PatrolState(_enemyAI, _enemyStateMachine));
            }
        }

        public void Exit()
        {
        }
    }
}