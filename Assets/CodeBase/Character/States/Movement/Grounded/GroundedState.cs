using Assets.CodeBase.Character.States.Movement.Grounded.Moving;

namespace Assets.CodeBase.Character.States.Movement.Grounded
{
    public class GroundedState : MovementState
    {
        public GroundedState(MovementStateMachine stateMachine) : base(stateMachine) {
        }

        protected override void AddInputActionsCallbacks() {
            base.AddInputActionsCallbacks();

            _stateMachine.Player.InputService.MovementCancelled += OnMovementCancelled;
        }

        protected override void RemoveInputActionsCallbacks() {
            base.RemoveInputActionsCallbacks();

            _stateMachine.Player.InputService.MovementCancelled -= OnMovementCancelled;
        }

        protected virtual void OnMovementCancelled() {
            _stateMachine.Enter<IdlingState>();
        }

        protected virtual void OnMove() {
            if (_stateMachine.ReusableData.IsWalking) {
                _stateMachine.Enter<WalkingState>();
            } else {
                _stateMachine.Enter<RunningState>();
            }
        }
    }
}
