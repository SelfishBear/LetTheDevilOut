using CodeBase.Audio;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Logic.Killer
{
    public class RetreatState : IEnemyState
    {
        private readonly EnemyAI _enemyAI;
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly EnemyAnimator _enemyAnimator;
        private readonly KillerScreamSound _soundPlayer;

        public RetreatState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine, EnemyAnimator enemyAnimator, KillerScreamSound soundPlayer)
        {
            _enemyAI = enemyAI;
            _enemyStateMachine = enemyStateMachine;
            _enemyAnimator = enemyAnimator;
            _soundPlayer = soundPlayer;
        }

        public void Enter()
        {
            _enemyAI.NavMeshAgent.speed = _enemyAI.RunSpeed;
            
            _enemyAnimator.Run(true);
            
            Transform farthestPoint = _enemyAI.GetFarthestPatrolPointFromPlayer();
            if (farthestPoint != null)
            {
                _enemyAI.NavMeshAgent.SetDestination(farthestPoint.position);
            }
            
            Debug.Log("Enter Retreat State");
        }

        public void Update()
        {
            if (_enemyAI.NavMeshAgent.remainingDistance < 0.5f)
            {
                _enemyStateMachine.ChangeState(new PatrolState(_enemyAI, _enemyStateMachine, _enemyAnimator, _soundPlayer));
            }
        }

        public void Exit()
        {
            _enemyAnimator.Run(false);
            _enemyAI.NavMeshAgent.speed = _enemyAI.PatrolSpeed;
        }
    }
}

