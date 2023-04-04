namespace Assets.CodeBase.Character.States.Movement.Grounded.Stopping
{
    public class StoppingState : GroundedState
    {
        public StoppingState(MovementStateMachine stateMachine) : base(stateMachine) {
        }

        public override void Enter() {
            base.Enter();

            _stateMachine.ReusableData.MovementSpeedModifier = 0f;
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            if (!IsMovingHorizontally())
                return;

            DecelerateHorizontally();
        }

        protected override void AddInputActionsCallbacks() {
            base.AddInputActionsCallbacks();

            _stateMachine.Player.InputService.MovementStarted += OnMovementStarted;
        }

        protected override void RemoveInputActionsCallbacks() {
            base.RemoveInputActionsCallbacks();

            _stateMachine.Player.InputService.MovementStarted -= OnMovementStarted;
        }

        private void OnMovementStarted() {
            OnMove();
        }

        public override void OnAnimationTransitEvent() {
            _stateMachine.Enter<IdlingState>();
        }
    }
}
