using Assets.CodeBase.Character.States.Movement.Grounded.Moving;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Stopping
{
    public class HardStoppingState : StoppingState
    {
        public HardStoppingState(MovementStateMachine stateMachine) : base(stateMachine) {
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_stateMachine.Player.AnimationData.HardStopParameterHash);

            _stateMachine.ReusableData.MovementDecelerationForce =
                _stateMachine.Player.Data.GroundedData.StopData.HardDecelerationForce;
            _stateMachine.ReusableData.CurrentJumpForce = _airborneData.JumpData.StrongForce;
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_stateMachine.Player.AnimationData.HardStopParameterHash);
        }

        protected override void OnMove() {
            if (_stateMachine.ReusableData.IsWalking)
                return;

            _stateMachine.Enter<RunningState>();
        }

        protected override void ExitByTime() { }
    }
}
