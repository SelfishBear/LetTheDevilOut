using UnityEngine;

namespace CodeBase.Logic.Killer
{
    public class PatrolState : IEnemyState
    {
        private readonly EnemyAI _enemyAI;
        private readonly EnemyStateMachine _enemyStateMachine;
        private int _currentPointIndex;

        public PatrolState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine)
        {
            _enemyAI = enemyAI;
            _enemyStateMachine = enemyStateMachine;
        }

        public void Enter()
        { 
            _enemyAI.NavMeshAgent.speed = _enemyAI.PatrolSpeed;
            MoveToNextPoint();
            Debug.Log("Enter Patrol State");
        }

        public void Update()
        {
            if (_enemyAI.CanSeePlayer())
            {
                _enemyStateMachine.ChangeState<ChaseState>(new ChaseState(_enemyAI, _enemyStateMachine));
                return;
            }

            if (_enemyAI.NavMeshAgent.remainingDistance < 0.5f)
            {
                MoveToNextPoint();
            }
        }

        public void Exit()
        {
        }

        private void MoveToNextPoint()
        {
            if (_enemyAI.PatrolPoints.Length == 0) return;
            _enemyAI.NavMeshAgent.SetDestination(_enemyAI.PatrolPoints[_currentPointIndex].position);
            _currentPointIndex = (_currentPointIndex + 1) % _enemyAI.PatrolPoints.Length;
        }
    }
}