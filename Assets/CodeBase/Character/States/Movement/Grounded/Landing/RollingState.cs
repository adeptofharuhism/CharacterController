using Assets.CodeBase.Character.Data.States.Grounded.Landing;
using Assets.CodeBase.Character.States.Movement.Grounded.Stopping;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Landing
{
    public class RollingState : LandingState
    {
        private UnitRollData _rollData;

        public RollingState(MovementStateConstructionData constructionData, Transform unitTransform) : 
            base(constructionData, unitTransform) {

            _rollData = _groundedData.RollData;
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_animationData.RollParameterHash);

            _reusableData.MovementSpeedModifier = _rollData.SpeedModifier;

            _reusableData.IsSprinting = false;
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_animationData.RollParameterHash);
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            if (_reusableData.MovementInput != Vector2.zero)
                return;

            RotateTowardsTargetRotation();
        }

        public override void OnAnimationTransitEvent() {
            if (_reusableData.MovementInput == Vector2.zero) {
                _stateMachine.Enter<MediumStoppingState>();
                return;
            }

            OnMove();
        }

        protected override void OnJumpStarted() { }
    }
}
