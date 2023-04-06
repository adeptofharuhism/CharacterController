using Assets.CodeBase.Character.Data.States.Airborne;
using Assets.CodeBase.Character.Data.States.Grounded;
using Assets.CodeBase.Infrastructure;
using System;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement
{
    public class MovementState : IUnitState
    {
        protected readonly MovementStateMachine _stateMachine;

        protected UnitGroundedData _groundedData;
        protected UnitAirborneData _airborneData;

        public MovementState(MovementStateMachine stateMachine) {
            _stateMachine = stateMachine;

            _groundedData = _stateMachine.Player.Data.GroundedData;
            _airborneData = _stateMachine.Player.Data.AirborneData;

            InitializeData();
        }

        public virtual void Enter() {
            Debug.Log($"State: {GetType().Name}");

            AddInputActionsCallbacks();
        }

        public virtual void Exit() {
            RemoveInputActionsCallbacks();
        }

        public virtual void HandleInput() {
            ReadMovementInput();
        }

        public virtual void Update() {

        }

        public virtual void PhysicsUpdate() {
            Move();
        }

        public virtual void OnAnimationEnterEvent() { }

        public virtual void OnAnimationExitEvent() { }

        public virtual void OnAnimationTransitEvent() { }

        public virtual void OnTriggerEnter(Collider collider) {
            if (_stateMachine.Player.LayerData.IsGroundLayer(collider.gameObject.layer))
                OnContactWithGround(collider);
        }

        public virtual void OnTriggerExit(Collider collider) { }

        private void InitializeData() => SetBaseRotationData();

        private void ReadMovementInput() {
            _stateMachine.ReusableData.MovementInput = _stateMachine.Player.InputService.MoveInputValue;
        }

        private void Move() {
            if (_stateMachine.ReusableData.MovementInput == Vector2.zero || _stateMachine.ReusableData.MovementSpeedModifier == 0f)
                return;

            Vector3 movementDirection = GetMovementDirection();

            float targetRotationYAngle = Rotate(movementDirection);
            Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

            float movementSpeed = GetMovementSpeed();

            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
            _stateMachine.Player.Rigidbody.AddForce(
                targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity,
                ForceMode.VelocityChange);
        }

        private float Rotate(Vector3 direction) {
            float directionAngle = UpdateTargetRotation(direction);

            RotateTowardsTargetRotation();

            return directionAngle;
        }

        private void UpdateTargetRotationData(float targetAngle) {
            _stateMachine.ReusableData.CurrentTargetRotation.y = targetAngle;

            _stateMachine.ReusableData.DampedTargetRotationPassedTime.y = 0f;
        }

        private static float GetDirectionAngle(Vector3 direction) {
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            if (directionAngle < 0f)
                directionAngle += 360f;
            return directionAngle;
        }

        private float AddCameraRotationToAngle(float directionAngle) {
            directionAngle += _stateMachine.Player.MainCameraTransform.eulerAngles.y;

            if (directionAngle >= 360f)
                directionAngle -= 360f;
            return directionAngle;
        }

        protected void SetBaseRotationData() {
            _stateMachine.ReusableData.RotationData = _groundedData.BaseRotationData;
            _stateMachine.ReusableData.TimeToReachTargetRotation = _groundedData.BaseRotationData.TargetRotationReachTime;
        }

        protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true) {
            float directionAngle = GetDirectionAngle(direction);

            if (shouldConsiderCameraRotation)
                directionAngle = AddCameraRotationToAngle(directionAngle);

            if (directionAngle != _stateMachine.ReusableData.CurrentTargetRotation.y)
                UpdateTargetRotationData(directionAngle);

            return directionAngle;
        }

        protected Vector3 GetTargetRotationDirection(float targetYAngle) =>
            Quaternion.Euler(0f, targetYAngle, 0f) * Vector3.forward;

        protected void RotateTowardsTargetRotation() {
            float currentYAngle = _stateMachine.Player.Rigidbody.rotation.y;

            if (currentYAngle == _stateMachine.ReusableData.CurrentTargetRotation.y)
                return;

            float smoothedYAngle = Mathf.SmoothDampAngle(
                currentYAngle,
                _stateMachine.ReusableData.CurrentTargetRotation.y,
                ref _stateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y,
                _stateMachine.ReusableData.TimeToReachTargetRotation.y - _stateMachine.ReusableData.DampedTargetRotationPassedTime.y);

            _stateMachine.ReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

            _stateMachine.Player.Rigidbody.MoveRotation(targetRotation);
        }

        protected Vector3 GetPlayerHorizontalVelocity() {
            Vector3 playerHorizontalVelocity = _stateMachine.Player.Rigidbody.velocity;
            playerHorizontalVelocity.y = 0;

            return playerHorizontalVelocity;
        }

        protected void DecelerateHorizontally() {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            _stateMachine.Player.Rigidbody.AddForce(
                -playerHorizontalVelocity * _stateMachine.ReusableData.MovementDecelerationForce,
                ForceMode.Acceleration);
        }

        protected void DecelerateVertically() {
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();

            _stateMachine.Player.Rigidbody.AddForce(
                -playerVerticalVelocity * _stateMachine.ReusableData.MovementDecelerationForce,
                ForceMode.Acceleration);
        }

        protected bool IsMovingHorizontally(float minimalMagnitude = .14f) {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();
            Vector2 playerHorizontalMovement = new Vector2(playerHorizontalVelocity.x, playerHorizontalVelocity.z);

            return playerHorizontalMovement.sqrMagnitude > minimalMagnitude;
        }

        protected bool IsMovingUp(float minimalVelocity = .1f) =>
            GetPlayerVerticalVelocity().y > minimalVelocity;

        protected bool IsMovingDown(float minimalVelocity = .1f) =>
            GetPlayerVerticalVelocity().y < minimalVelocity;

        protected Vector3 GetPlayerVerticalVelocity() =>
            new Vector3(0f, _stateMachine.Player.Rigidbody.velocity.y, 0f);

        protected float GetMovementSpeed() =>
            _groundedData.BaseSpeed * _stateMachine.ReusableData.MovementOnSlopesSpeedModifier * _stateMachine.ReusableData.MovementSpeedModifier;

        protected Vector3 GetMovementDirection() =>
            new Vector3(_stateMachine.ReusableData.MovementInput.x, 0f, _stateMachine.ReusableData.MovementInput.y);

        protected void ResetVelocity() =>
            _stateMachine.Player.Rigidbody.velocity = Vector3.zero;

        protected virtual void OnContactWithGround(Collider collider) { }

        protected virtual void AddInputActionsCallbacks() => 
            _stateMachine.Player.InputService.WalkToggleTriggered += WalkToggle;

        protected virtual void RemoveInputActionsCallbacks() => 
            _stateMachine.Player.InputService.WalkToggleTriggered -= WalkToggle;

        protected virtual void WalkToggle() =>
            _stateMachine.ReusableData.IsWalking = !_stateMachine.ReusableData.IsWalking;
    }
}
