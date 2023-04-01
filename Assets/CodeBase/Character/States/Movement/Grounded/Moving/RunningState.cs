namespace Assets.CodeBase.Character.States.Movement.Grounded.Moving
{
    public class RunningState : MovementState
    {
        public RunningState(MovementStateMachine stateMachine) : base(stateMachine) {
        }

        public override void Enter() {
            base.Enter();

            _speedModifier = 1f;
        }

        protected override void AddInputActionsCallbacks() {
            base.AddInputActionsCallbacks();

            _stateMachine.Player.InputService.MovementCancelled += OnMovementCancelled;
        }

        protected override void RemoveInputActionsCallbacks() {
            base.RemoveInputActionsCallbacks();

            _stateMachine.Player.InputService.MovementCancelled -= OnMovementCancelled;
        }

        protected void OnMovementCancelled() {
            _stateMachine.Enter<IdlingState>();
        }

        protected override void WalkToggle() {
            base.WalkToggle();

            _stateMachine.Enter<WalkingState>();
        }
    }
}
