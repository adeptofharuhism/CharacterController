using Assets.CodeBase.Character.States.Movement.Grounded.Moving;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded
{
    public class IdlingState : MovementState
    {
        public IdlingState(MovementStateMachine stateMachine) : base(stateMachine) {
        }

        public override void Enter() {
            base.Enter();

            _speedModifier = 0f;

            ResetVelocity();
        }

        public override void Update() {
            base.Update();

            if (_movementInput == Vector2.zero)
                return;

            OnMove();
        }

        private void OnMove() {
            if (_isWalking) {
                _stateMachine.Enter<WalkingState>();
            } else {
                _stateMachine.Enter<RunningState>();
            }
        }
    }
}
