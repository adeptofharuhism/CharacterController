using Assets.CodeBase.Character.States.Movement.Grounded.Stopping;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Moving
{
    public class WalkingState : MovingState
    {
        public WalkingState(MovementStateMachine stateMachine) : base(stateMachine) {
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_stateMachine.Player.AnimationData.WalkParameterHash);

            _stateMachine.ReusableData.MovementSpeedModifier = _groundedData.WalkData.SpeedModifier;
            _stateMachine.ReusableData.CurrentJumpForce = _airborneData.JumpData.WeakForce;
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_stateMachine.Player.AnimationData.WalkParameterHash);
        }

        protected override void OnMovementCancelled() {
            _stateMachine.Enter<LightStoppingState>();
        }

        protected override void WalkToggle() {
            base.WalkToggle();

            _stateMachine.Enter<RunningState>();
        }
    }
}
