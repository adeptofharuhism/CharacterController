namespace Assets.CodeBase.Character.States.Movement.Grounded.Landing
{
    public class LandingState : GroundedState
    {
        public LandingState(MovementStateMachine stateMachine) : base(stateMachine) {
        }

        protected override void OnMovementCancelled() {
        }
    }
}
