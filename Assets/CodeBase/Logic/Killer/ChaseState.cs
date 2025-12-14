using CodeBase.Audio;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Logic.Killer
{
    public class ChaseState : IEnemyState
    {
        private readonly EnemyAI _enemyAI;
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly EnemyAnimator _enemyAnimator;
        private readonly KillerScreamSound _soundPlayer;

        public ChaseState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine, EnemyAnimator enemyAnimator, KillerScreamSound soundPlayer)
        {
            _enemyAI = enemyAI;
            _enemyStateMachine = enemyStateMachine;
            _enemyAnimator = enemyAnimator;
            _soundPlayer = soundPlayer;
        }
        public void Enter()
        {
            _enemyAI.NavMeshAgent.speed = _enemyAI.RunSpeed;
            _soundPlayer.PlayScreamSound();
            Debug.Log("Enter Chase State");
        }

        public void Update()
        {
            _enemyAnimator.Run(true);
            _enemyAI.NavMeshAgent.SetDestination(_enemyAI.TargetPlayer.transform.position);

            if (_enemyAI.CanAttackPlayer())
            {
                _enemyStateMachine.ChangeState<AttackState>(new AttackState(_enemyAI, _enemyStateMachine, _enemyAnimator, _soundPlayer));
            }

            if (!_enemyAI.CanSeePlayer())
            {
                _enemyStateMachine.ChangeState<PatrolState>(new PatrolState(_enemyAI, _enemyStateMachine, _enemyAnimator, _soundPlayer));
            }
        }

        public void Exit()
        {
            _soundPlayer.StopScreamSound();
            _enemyAnimator.Run(false);
        }
    }
}