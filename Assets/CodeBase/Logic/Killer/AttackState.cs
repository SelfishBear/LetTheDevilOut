using UnityEngine;

namespace CodeBase.Logic.Killer
{
    public class AttackState : IEnemyState
    {
        private readonly EnemyAI _enemyAI;
        private readonly EnemyStateMachine _enemyStateMachine;

        public AttackState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine)
        {
            _enemyAI = enemyAI;
            _enemyStateMachine = enemyStateMachine;
        }

        public void Enter()
        {
            _enemyAI.NavMeshAgent.isStopped = true;
            Debug.Log("Enter Attack State");
        }

        public void Update()
        {
            //TODO: ATTACK PLAYER LOGIC

            if (!_enemyAI.CanAttackPlayer())
            {
                _enemyStateMachine.ChangeState(new ChaseState(_enemyAI, _enemyStateMachine));
            }
        }

        public void Exit()
        {
            _enemyAI.NavMeshAgent.isStopped = false;
        }
    }
}