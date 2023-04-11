using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded
{
    public class IdlingState : GroundedState
    {
        public IdlingState(MovementStateConstructionData constructionData, Transform unitTransform) : 
            base(constructionData, unitTransform) {
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_animationData.IdleParameterHash);

            _reusableData.MovementSpeedModifier = 0f;
            _reusableData.CurrentJumpForce = _airborneData.JumpData.StationaryForce;

            ResetVelocity();
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_animationData.IdleParameterHash);
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
    }
}
