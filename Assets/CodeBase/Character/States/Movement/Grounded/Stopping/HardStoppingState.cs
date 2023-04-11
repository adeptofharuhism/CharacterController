using Assets.CodeBase.Character.States.Movement.Grounded.Moving;
using Assets.CodeBase.Utility.Colliders;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Stopping
{
    public class HardStoppingState : StoppingState
    {
        public HardStoppingState(MovementStateConstructionData constructionData, Transform unitTransform) :
            base(constructionData, unitTransform) {
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_animationData.HardStopParameterHash);

            _reusableData.MovementDecelerationForce =
                _groundedData.StopData.HardDecelerationForce;
            _reusableData.CurrentJumpForce = _airborneData.JumpData.StrongForce;
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_animationData.HardStopParameterHash);
        }

        protected override void OnMove() {
            if (_reusableData.IsWalking)
                return;

            _stateMachine.Enter<RunningState>();
        }

        protected override void ExitByTime() { }
    }
}
