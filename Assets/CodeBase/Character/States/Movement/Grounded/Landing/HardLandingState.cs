using Assets.CodeBase.Character.States.Movement.Grounded.Moving;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Landing
{
    public class HardLandingState : LandingState
    {
        public HardLandingState(MovementStateMachine stateMachine) : base(stateMachine) {
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_stateMachine.Player.AnimationData.HardLandParameterHash);

            _stateMachine.Player.InputService.DisableMove();
            _stateMachine.ReusableData.MovementSpeedModifier = 0f;

            ResetVelocity();
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_stateMachine.Player.AnimationData.HardLandParameterHash);

            _stateMachine.Player.InputService.Enable();
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            if (!IsMovingHorizontally())
                return;

            ResetVelocity();
        }

        public override void OnAnimationExitEvent() => 
            _stateMachine.Player.InputService.Enable();

        public override void OnAnimationTransitEvent() =>
            _stateMachine.Enter<IdlingState>();

        protected override void AddInputActionsCallbacks() {
            base.AddInputActionsCallbacks();

            _stateMachine.Player.InputService.MovementStarted += OnMovementStarted;
        }

        protected override void RemoveInputActionsCallbacks() {
            base.RemoveInputActionsCallbacks();

            _stateMachine.Player.InputService.MovementStarted -= OnMovementStarted;
        }

        protected override void OnMove() {
            if (_stateMachine.ReusableData.IsWalking)
                return;

            _stateMachine.Enter<RunningState>();
        }

        protected override void OnJumpStarted() { }

        private void OnMovementStarted() {
            OnMove();
        }
    }
}
