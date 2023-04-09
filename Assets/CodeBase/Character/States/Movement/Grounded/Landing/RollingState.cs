using Assets.CodeBase.Character.Data.States.Grounded.Landing;
using Assets.CodeBase.Character.States.Movement.Grounded.Stopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Landing
{
    public class RollingState : LandingState
    {
        private UnitRollData _rollData;

        public RollingState(MovementStateMachine stateMachine) : base(stateMachine) {
            _rollData = _groundedData.RollData;
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_stateMachine.Player.AnimationData.RollParameterHash);

            _stateMachine.ReusableData.MovementSpeedModifier = _rollData.SpeedModifier;

            _stateMachine.ReusableData.IsSprinting = false;
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_stateMachine.Player.AnimationData.RollParameterHash);
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            if (_stateMachine.ReusableData.MovementInput != Vector2.zero)
                return;

            RotateTowardsTargetRotation();
        }

        public override void OnAnimationTransitEvent() {
            if (_stateMachine.ReusableData.MovementInput == Vector2.zero) {
                _stateMachine.Enter<MediumStoppingState>();
                return;
            }

            OnMove();
        }

        protected override void OnJumpStarted() { }
    }
}
