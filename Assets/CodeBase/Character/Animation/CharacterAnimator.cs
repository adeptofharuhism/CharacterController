using System;
using UnityEngine;

namespace Assets.CodeBase.Character.Animation
{
    public class CharacterAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterMovement _characterMovement;

        //Animator parameter hashes
        public static readonly int ParameterSpeedHash = Animator.StringToHash("Speed");

        //Animator state hashes
        public static readonly int StateIdleHash = Animator.StringToHash("Idle");
        public static readonly int StateMoveHash = Animator.StringToHash("Move");

        private CharacterAnimationState _state;

        public event Action<CharacterAnimationState> StateEntered;
        public event Action<CharacterAnimationState> StateExited;

        public CharacterAnimationState State => _state;

        private void Update() {
            _animator.SetFloat(ParameterSpeedHash, _characterMovement.CurrentMovespeedPart, 0.1f, Time.deltaTime);
        }

        public void EnteredState(int stateHash) {
            _state = StateFor(stateHash);
            StateEntered?.Invoke(_state);
        }

        public void ExitedState(int stateHash) =>
            StateExited?.Invoke(_state);

        private CharacterAnimationState StateFor(int stateHash) {
            CharacterAnimationState state;
            if (stateHash == StateIdleHash)
                state = CharacterAnimationState.Idle;
            else if (stateHash == StateMoveHash)
                state = CharacterAnimationState.Move;
            else
                state = CharacterAnimationState.Unknown;

            return state;
        }
    }
}