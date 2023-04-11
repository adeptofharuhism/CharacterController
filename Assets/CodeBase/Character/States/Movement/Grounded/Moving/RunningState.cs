using Assets.CodeBase.Character.States.Movement.Grounded.Stopping;
using Assets.CodeBase.Utility.Colliders;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Moving
{
    public class RunningState : MovingState
    {
        private float _startTime;

        public RunningState(MovementStateConstructionData constructionData, Transform unitTransform) : 
            base(constructionData, unitTransform) {
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_animationData.RunParameterHash);

            _reusableData.MovementSpeedModifier = _groundedData.RunData.SpeedModifier;
            _reusableData.CurrentJumpForce = _airborneData.JumpData.MediumForce;

            _startTime = Time.time;
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_animationData.RunParameterHash);
        }

        public override void Update() {
            base.Update();

            if (!_reusableData.IsWalking)
                return;

            if (Time.time > _startTime + _groundedData.RunData.SpeedModifier)
                StopRunning();
        }

        private void StopRunning() {
            if (_reusableData.MovementInput == Vector2.zero)
                _stateMachine.Enter<IdlingState>();
            else _stateMachine.Enter<WalkingState>();
        }

        protected override void OnMovementCancelled() {
            _stateMachine.Enter<MediumStoppingState>();
        }

        protected override void WalkToggle() {
            base.WalkToggle();

            _stateMachine.Enter<WalkingState>();
        }
    }
}
