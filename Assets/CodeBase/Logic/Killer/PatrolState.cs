using CodeBase.Audio;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Logic.Killer
{
    public class PatrolState : IEnemyState
    {
        private readonly EnemyAI _enemyAI;
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly EnemyAnimator _enemyAnimator;
        private readonly KillerScreamSound _soundPlayer;
        private int _currentPointIndex;

        public PatrolState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine, EnemyAnimator enemyAnimator, KillerScreamSound soundPlayer)
        {
            _enemyAI = enemyAI;
            _enemyStateMachine = enemyStateMachine;
            _enemyAnimator = enemyAnimator;
            _soundPlayer = soundPlayer;
        }

        public void Enter()
        { 
            _enemyAnimator.Patrol(true);
            _enemyAI.NavMeshAgent.speed = _enemyAI.PatrolSpeed;
            MoveToNextPoint();
            Debug.Log("Enter Patrol State");
        }

        public void Update()
        {
            if (_enemyAI.CanSeePlayer())
            {
                _enemyStateMachine.ChangeState<ChaseState>(new ChaseState(_enemyAI, _enemyStateMachine, _enemyAnimator, _soundPlayer));
                return;
            }

            if (_enemyAI.NavMeshAgent.remainingDistance < 0.5f)
            {
                MoveToNextPoint();
            }
        }

        public void Exit()
        {
            _enemyAnimator.Patrol(false);
        }

        private void MoveToNextPoint()
        {
            if (_enemyAI.PatrolPoints.Length == 0) return;
            _enemyAI.NavMeshAgent.SetDestination(_enemyAI.PatrolPoints[_currentPointIndex].position);
            _currentPointIndex = (_currentPointIndex + 1) % _enemyAI.PatrolPoints.Length;
        }
    }
}