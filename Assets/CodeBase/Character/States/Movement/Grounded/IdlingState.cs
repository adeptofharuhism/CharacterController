using Assets.CodeBase.Character.States.Movement.Grounded.Moving;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded
{
    public class IdlingState : GroundedState
    {
        public IdlingState(MovementStateMachine stateMachine) : base(stateMachine) {
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_stateMachine.Player.AnimationData.IdleParameterHash);

            _stateMachine.ReusableData.MovementSpeedModifier = 0f;
            _stateMachine.ReusableData.CurrentJumpForce = _airborneData.JumpData.StationaryForce;

            ResetVelocity();
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_stateMachine.Player.AnimationData.IdleParameterHash);
        }

        public override void Update() {
            base.Update();

            if (_stateMachine.ReusableData.MovementInput == Vector2.zero)
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
