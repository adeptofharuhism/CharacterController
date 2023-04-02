namespace Assets.CodeBase.Character.States.Movement.Grounded.Moving
{
    public class RunningState : MovingState
    {
        public RunningState(MovementStateMachine stateMachine) : base(stateMachine) {
        }

        public override void Enter() {
            base.Enter();

            _stateMachine.ReusableData.MovementSpeedModifier = _groundedData.RunData.SpeedModifier;
        }

        protected override void WalkToggle() {
            base.WalkToggle();

            _stateMachine.Enter<WalkingState>();
        }
    }
}
