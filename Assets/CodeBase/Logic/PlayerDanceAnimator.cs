using System;
using UnityEngine;

namespace CodeBase.Logic
{
    public class PlayerDanceAnimator : MonoBehaviour
    {
        [SerializeField] public Animator _animator;
        
        private static readonly int Dance1 = Animator.StringToHash("Dance1");
        private static readonly int Dance2 = Animator.StringToHash("Dance2");
        private static readonly int Dance3 = Animator.StringToHash("Dance3");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _idleStateFullHash = Animator.StringToHash("Base Layer.Idle");
        private readonly int _attackStateHash = Animator.StringToHash("Attack Normal");
        private readonly int _walkingStateHash = Animator.StringToHash("Run");
        private readonly int _deathStateHash = Animator.StringToHash("Die");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }
        public bool IsAttacking => State == AnimatorState.Attack;
        
        public void PlayDance(int danceIndex)
        {
            switch (danceIndex)
            {
                case 1:
                    _animator.Play(Dance1);
                    break;
                case 2:
                    _animator.Play(Dance2);
                    break;
                case 3:
                    _animator.Play(Dance3);
                    break;
                default:
                    Debug.LogWarning("Invalid dance index: " + danceIndex);
                    break;
            }
        }

        public void ResetToIdle()
        {
            _animator.Play(_idleStateHash, -1);
        }

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash)
        {
            StateExited?.Invoke(StateFor(stateHash));
        }

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == _idleStateHash)
            {
                state = AnimatorState.Idle;
            }
            else if (stateHash == _attackStateHash)
            {
                state = AnimatorState.Attack;
            }
            else if (stateHash == _walkingStateHash)
            {
                state = AnimatorState.Walking;
            }
            else if (stateHash == _deathStateHash)
            {
                state = AnimatorState.Died;
            }
            else
            {
                state = AnimatorState.Unknown;
            }

            return state;
        }
    }
}