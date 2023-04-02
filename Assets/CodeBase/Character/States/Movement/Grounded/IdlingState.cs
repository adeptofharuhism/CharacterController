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

            _stateMachine.ReusableData.MovementSpeedModifier = 0f;

            ResetVelocity();
        }

        public override void Update() {
            base.Update();

            if (_stateMachine.ReusableData.MovementInput == Vector2.zero)
                return;

            OnMove();
        }
    }
}
