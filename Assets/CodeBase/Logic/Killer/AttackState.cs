using System.Collections;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Logic.Killer
{
    public class AttackState : IEnemyState
    {
        private readonly EnemyAI _enemyAI;
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly EnemyAnimator _enemyAnimator;
        private bool _hasAttacked;
        private Coroutine _attackCoroutine;
        private float _delayAfterAttack = 2f;
        private float _lungeSpeed = 15f;
        private float _stopDistance = 1.3f;

        public AttackState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine, EnemyAnimator enemyAnimator)
        {
            _enemyAI = enemyAI;
            _enemyStateMachine = enemyStateMachine;
            _enemyAnimator = enemyAnimator;
        }

        public void Enter()
        {
            _hasAttacked = false;
            Debug.Log("Enter Attack State");
        }

        public void Update()
        {
            if (!_hasAttacked)
            {
                _hasAttacked = true;
                _enemyAnimator.PlayAttack();
            }

            MoveAndRotateTowardsPlayer();
        }

        private void MoveAndRotateTowardsPlayer()
        {
            Transform killerTransform = _enemyAI.transform;
            Transform playerTransform = _enemyAI.TargetPlayer.transform;
            
            Vector3 directionToPlayer = playerTransform.position - killerTransform.position;
            directionToPlayer.y = 0f; // Игнорируем разницу по высоте для поворота
            
            if (directionToPlayer.sqrMagnitude > 0.001f)
            {
                // Плавный поворот через кватернион (только по Y оси)
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer.normalized);
                killerTransform.rotation = Quaternion.Slerp(killerTransform.rotation, targetRotation, Time.deltaTime * 10f);
            }
            
            // Плавное движение к игроку с рывком
            Vector3 targetPosition = playerTransform.position - directionToPlayer.normalized * _stopDistance;
            targetPosition.y = killerTransform.position.y; // Сохраняем высоту киллера
            
            float distanceToTarget = Vector3.Distance(killerTransform.position, targetPosition);
            if (distanceToTarget > 0.1f)
            {
                killerTransform.position = Vector3.MoveTowards(killerTransform.position, targetPosition, _lungeSpeed * Time.deltaTime);
            }
        }

        public void Exit()
        {
            // if (_attackCoroutine != null)
            // {
            //     _enemyAI.StopCoroutine(_attackCoroutine);
            //     _attackCoroutine = null;
            // }
        }
    }
}