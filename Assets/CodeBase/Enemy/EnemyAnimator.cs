using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int IsPatrolling = Animator.StringToHash("IsPatrolling");
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");

        private readonly int _idleStateHash = Animator.StringToHash("idle");
        private readonly int _attackStateHash = Animator.StringToHash("attack01");
        private readonly int _jumpAttackStateHash = Animator.StringToHash("jumpAttack");
        private readonly int _walkingStateHash = Animator.StringToHash("Move");
        private readonly int _deathStateHash = Animator.StringToHash("die");
        private readonly int _getHitStateHash = Animator.StringToHash("GetHit");
        private readonly int _spawnStateHash = Animator.StringToHash("Spawn");

        private Animator _animator;

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }
        
        public Animator Animator => _animator;

        private void Awake() =>
            _animator = GetComponent<Animator>();

        public void Patrol(bool isPatrolling)
        {
            _animator.SetBool(IsPatrolling, isPatrolling);
        }

        public void Run(bool isRunning)
        {
            _animator.SetBool(IsRunning, isRunning);
        }

        public void PlayAttack()
        {
            _animator.SetTrigger(Attack);
        }

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) =>
            StateExited?.Invoke(StateFor(stateHash));

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _attackStateHash || stateHash == _jumpAttackStateHash)
                state = AnimatorState.Attack;
            else if (stateHash == _walkingStateHash)
                state = AnimatorState.Walking;
            else if (stateHash == _deathStateHash)
                state = AnimatorState.Died;
            else if (stateHash == _getHitStateHash)
                state = AnimatorState.Hit;
            else if (stateHash == _spawnStateHash)
                state = AnimatorState.Spawning;
            else
                state = AnimatorState.Unknown;

            return state;
        }
    }
}