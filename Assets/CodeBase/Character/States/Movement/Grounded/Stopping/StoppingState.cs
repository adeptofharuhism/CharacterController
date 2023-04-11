using Assets.CodeBase.Utility.Colliders;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Stopping
{
    public class StoppingState : GroundedState
    {
        private float _exitTime;

        public StoppingState(MovementStateConstructionData constructionData, Transform unitTransform) :
            base(constructionData, unitTransform) {
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_animationData.StoppingParameterHash);

            _reusableData.MovementSpeedModifier = 0f;

            _exitTime = Time.time + _groundedData.StopData.ForceStopStateExitTime;
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_animationData.StoppingParameterHash);
        }

        public override void Update() {
            base.Update();

            ExitByTime();
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            RotateTowardsTargetRotation();

            if (!IsMovingHorizontally())
                return;

            DecelerateHorizontally();
        }

        protected override void AddInputActionsCallbacks() {
            base.AddInputActionsCallbacks();

            _inputService.MovementStarted += OnMovementStarted;
        }

        protected override void RemoveInputActionsCallbacks() {
            base.RemoveInputActionsCallbacks();

            _inputService.MovementStarted -= OnMovementStarted;
        }

        public override void OnAnimationTransitEvent() => _stateMachine.Enter<IdlingState>();

        protected virtual void ExitByTime() {
            if (_exitTime < Time.time)
                _stateMachine.Enter<IdlingState>();
        }

        private void OnMovementStarted() => OnMove();
    }
}
