namespace Assets.CodeBase.Character.States.Movement.Grounded.Moving
{
    public class MovingState : GroundedState
    {
        public MovingState(MovementStateMachine stateMachine) : base(stateMachine) {
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_stateMachine.Player.AnimationData.MovingParameterHash);
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_stateMachine.Player.AnimationData.MovingParameterHash);
        }
    }
}
