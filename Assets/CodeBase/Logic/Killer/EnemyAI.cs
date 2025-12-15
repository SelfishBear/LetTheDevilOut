using System;
using CodeBase.Audio;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic.Map;
using CodeBase.PlayerLogic;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Logic.Killer
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private KillerScreamSound _soundPlayer;
        [SerializeField] private KillerHitSound _hitSound;
        [SerializeField] private LayerMask _wallLayerMask;
        [SerializeField] private LayerMask _playerLayerMask;
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float _patrolSpeed = 2f;
        [SerializeField] private float _runSpeed = 5f;
        [SerializeField] private float _detectionRange = 10f;
        [SerializeField] private float _attackRange = 5f;
        [SerializeField] private float _detectionAngle = 45f;


        private EnemyStateMachine _enemyStateMachine;
        private Transform[] _patrolPoints;
        private PlayerPrefab _targetPlayer;
        public float PatrolSpeed => _patrolSpeed;
        public float RunSpeed => _runSpeed;
        public float DetectionRange => _detectionRange;
        public float DetectionAngle => _detectionAngle;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;

        public Transform[] PatrolPoints => _patrolPoints;

        public PlayerPrefab TargetPlayer => _targetPlayer;

        public void Construct(PatrolPoint[] patrolPoints)
        {
            _patrolPoints = Array.ConvertAll(patrolPoints, patrolPoint => patrolPoint.transform);
            foreach (var patrolPoint in _patrolPoints)
            {
                Debug.Log(patrolPoint.gameObject.name);
            }
        }

        private void Awake()
        {
            _targetPlayer = AllServices.Container.Single<IGameFactory>().HeroPrefab;
            _enemyStateMachine = new EnemyStateMachine();
        }

        private void Start()
        {
            _enemyStateMachine.ChangeState<PatrolState>(new PatrolState(this, _enemyStateMachine, _enemyAnimator,
                _soundPlayer));
        }

        private void Update()
        {
            _enemyStateMachine.Update();
        }

        private void OnDrawGizmos()
        {
            Vector3 origin = transform.position;

            Quaternion leftRotation = Quaternion.Euler(0, -_detectionAngle / 2, 0);
            Quaternion rightRotation = Quaternion.Euler(0, _detectionAngle / 2, 0);

            Vector3 leftDirection = leftRotation * transform.forward;
            Vector3 rightDirection = rightRotation * transform.forward;

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(origin, origin + leftDirection * _detectionRange);
            Gizmos.DrawLine(origin, origin + rightDirection * _detectionRange);

#if UNITY_EDITOR
            UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.DrawWireArc(origin, Vector3.up, leftDirection.normalized, _detectionAngle,
                _detectionRange);
#endif
        }

        public bool CanSeePlayer()
        {
            if (_targetPlayer == null)
                return false;

            Vector3 directionToPlayer = _targetPlayer.transform.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            if (distanceToPlayer > _detectionRange)
                return false;

            float angle = Vector3.Angle(transform.forward, directionToPlayer.normalized);
            if (angle > _detectionAngle / 2)
                return false;

            if (Physics.Raycast(transform.position, directionToPlayer.normalized, out RaycastHit hit, distanceToPlayer,
                    _wallLayerMask))
                return false;

            return true;
        }

        public bool CanAttackPlayer()
        {
            if (_targetPlayer == null)
                return false;

            Vector3 directionToPlayer = _targetPlayer.transform.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;
            if (distanceToPlayer > _attackRange)
                return false;

            return true;
        }

        public Transform GetFarthestPatrolPointFromPlayer()
        {
            if (_patrolPoints == null || _patrolPoints.Length == 0)
                return null;

            if (_targetPlayer == null)
                return _patrolPoints[0];

            Transform farthestPoint = null;
            float maxDistance = 0f;

            foreach (Transform point in _patrolPoints)
            {
                float distance = Vector3.Distance(point.position, _targetPlayer.transform.position);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    farthestPoint = point;
                }
            }

            return farthestPoint;
        }

        public void OnAttack()
        {
            _targetPlayer.PlayerHealth.TakeDamage(100);
            _hitSound.PlayKillerHitSound();
            _enemyStateMachine.ChangeState(new RetreatState(this, _enemyStateMachine, _enemyAnimator, _soundPlayer));
        }
    }
}