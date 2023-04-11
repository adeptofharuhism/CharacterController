using Assets.CodeBase.Character.Data.States.Grounded.Moving;
using Assets.CodeBase.Character.States.Movement.Grounded.Stopping;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Moving
{
    public class SprintingState : MovingState
    {
        private UnitSprintData _sprintData;

        private bool _keepSprinting = false;
        private bool _shouldResetSprintingState = false;
        private float _startTime;

        public SprintingState(MovementStateConstructionData constructionData, Transform unitTransform) : 
            base(constructionData, unitTransform) {

            _sprintData = _groundedData.SprintData;
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_animationData.SprintParameterHash);

            _reusableData.MovementSpeedModifier = _sprintData.SpeedModifier;
            _reusableData.CurrentJumpForce = _airborneData.JumpData.StrongForce;

            _shouldResetSprintingState = true;

            _startTime = Time.time;
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_animationData.SprintParameterHash);

            if (_shouldResetSprintingState) {
                _reusableData.IsSprinting = false;
                _keepSprinting = false;
            }
        }

        public override void Update() {
            base.Update();

            if (_keepSprinting)
                return;

            if (Time.time > _startTime + _sprintData.SprintToRunTime)
                StopSprinting();
        }

        private void StopSprinting() {
            if (_reusableData.MovementInput == Vector2.zero)
                _stateMachine.Enter<IdlingState>();
            else _stateMachine.Enter<RunningState>();
        }

        protected override void AddInputActionsCallbacks() {
            base.AddInputActionsCallbacks();

            _inputService.SprintPerformed += OnSprintPerformed;
        }

        protected override void RemoveInputActionsCallbacks() {
            base.RemoveInputActionsCallbacks();

            _inputService.SprintPerformed -= OnSprintPerformed;
        }

        private void OnSprintPerformed() {
            _keepSprinting = true;
            _reusableData.IsSprinting = true;
        }

        protected override void OnJumpStarted() {
            _shouldResetSprintingState = false;
            base.OnJumpStarted();
        }

        protected override void OnMovementCancelled() => 
            _stateMachine.Enter<HardStoppingState>();

        protected override void OnFall() {
            _shouldResetSprintingState = false;

            base.OnFall();
        }
    }
}
