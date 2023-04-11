using Assets.CodeBase.Character.States.Movement.Grounded.Stopping;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Moving
{
    public class WalkingState : MovingState
    {
        public WalkingState(MovementStateConstructionData constructionData, Transform unitTransform) : base(constructionData, unitTransform) {
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_animationData.WalkParameterHash);

            _reusableData.MovementSpeedModifier = _groundedData.WalkData.SpeedModifier;
            _reusableData.CurrentJumpForce = _airborneData.JumpData.WeakForce;
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_animationData.WalkParameterHash);
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
