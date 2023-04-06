namespace Assets.CodeBase.Character.States.Movement.Grounded.Stopping
{
    public class MediumStoppingState : StoppingState
    {
        public MediumStoppingState(MovementStateMachine stateMachine) : base(stateMachine) {
        }

        public override void Enter() {
            base.Enter();

            _stateMachine.ReusableData.MovementDecelerationForce =
                _stateMachine.Player.Data.GroundedData.StopData.MediumDecelerationForce;
            _stateMachine.ReusableData.CurrentJumpForce = _airborneData.JumpData.MediumForce;
        }
    }
}
