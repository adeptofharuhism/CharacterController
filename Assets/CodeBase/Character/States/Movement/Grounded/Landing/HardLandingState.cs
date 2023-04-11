using Assets.CodeBase.Character.States.Movement.Grounded.Moving;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Landing
{
    public class HardLandingState : LandingState
    {
        public HardLandingState(MovementStateConstructionData constructionData, Transform unitTransform) : 
            base(constructionData, unitTransform) {
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_animationData.HardLandParameterHash);

            _reusableData.MovementSpeedModifier = 0f;

            _inputService.DisableMove();

            ResetVelocity();
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_animationData.HardLandParameterHash);

            _inputService.Enable();
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            if (!IsMovingHorizontally())
                return;

            ResetVelocity();
        }

        public override void OnAnimationExitEvent() => 
            _inputService.Enable();

        public override void OnAnimationTransitEvent() =>
            _stateMachine.Enter<IdlingState>();

        protected override void AddInputActionsCallbacks() {
            base.AddInputActionsCallbacks();

            _inputService.MovementStarted += OnMovementStarted;
        }

        protected override void RemoveInputActionsCallbacks() {
            base.RemoveInputActionsCallbacks();

            _inputService.MovementStarted -= OnMovementStarted;
        }

        protected override void OnMove() {
            if (_reusableData.IsWalking)
                return;

            _stateMachine.Enter<RunningState>();
        }

        protected override void OnJumpStarted() { }

        private void OnMovementStarted() {
            OnMove();
        }
    }
}
