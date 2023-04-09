namespace Assets.CodeBase.Character.States.Movement.Grounded.Landing
{
    public class LandingState : GroundedState
    {
        public LandingState(MovementStateMachine stateMachine) : base(stateMachine) {
        }

        protected override void OnMovementCancelled() {
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_stateMachine.Player.AnimationData.LandingParameterHash);
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_stateMachine.Player.AnimationData.LandingParameterHash);
        }
    }
}
