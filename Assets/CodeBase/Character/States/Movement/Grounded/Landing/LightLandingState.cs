using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Landing
{
    public class LightLandingState : LandingState
    {
        public LightLandingState(MovementStateConstructionData constructionData, Transform unitTransform) : 
            base(constructionData, unitTransform) {
        }

        public override void Enter() {
            base.Enter();

            _reusableData.MovementSpeedModifier = 0f;
            _reusableData.CurrentJumpForce = _airborneData.JumpData.StationaryForce;

            ResetVelocity();
        }

        public override void Update() {
            base.Update();

            if (_reusableData.MovementInput == Vector2.zero)
                return;

            OnMove();
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            if (!IsMovingHorizontally())
                return;

            ResetVelocity();
        }

        public override void OnAnimationTransitEvent() => 
            _stateMachine.Enter<IdlingState>();
    }
}
