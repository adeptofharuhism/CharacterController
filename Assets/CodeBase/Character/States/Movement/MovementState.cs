using Assets.CodeBase.Character.Data.States.Grounded;
using Assets.CodeBase.Infrastructure;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement
{
    public class MovementState : IUnitState
    {
        protected readonly MovementStateMachine _stateMachine;

        protected UnitGroundedData _groundedData;

        public MovementState(MovementStateMachine stateMachine) {
            _stateMachine = stateMachine;

            _groundedData = _stateMachine.Player.Data.GroundedData;

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

        public virtual void OnAnimationEnterEvent() {

        }

        public virtual void OnAnimationExitEvent() {

        }

        public virtual void OnAnimationTransitEvent() {

        }

        private void InitializeData() {
            _stateMachine.ReusableData.TimeToReachTargetRotation = _groundedData.BaseRotationData.TargetRotationReachTime;
        }

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

        protected Vector3 GetPlayerVerticalVelocity() =>
            new Vector3(0f, _stateMachine.Player.Rigidbody.velocity.y, 0f);

        protected float GetMovementSpeed() =>
            _groundedData.BaseSpeed * _stateMachine.ReusableData.MovementOnSlopesSpeedModifier * _stateMachine.ReusableData.MovementSpeedModifier;

        protected Vector3 GetMovementDirection() =>
            new Vector3(_stateMachine.ReusableData.MovementInput.x, 0f, _stateMachine.ReusableData.MovementInput.y);

        protected void ResetVelocity() =>
            _stateMachine.Player.Rigidbody.velocity = Vector3.zero;

        protected virtual void AddInputActionsCallbacks() {
            _stateMachine.Player.InputService.WalkToggleTriggered += WalkToggle;
        }

        protected virtual void RemoveInputActionsCallbacks() {
            _stateMachine.Player.InputService.WalkToggleTriggered -= WalkToggle;
        }

        protected virtual void WalkToggle() =>
            _stateMachine.ReusableData.IsWalking = !_stateMachine.ReusableData.IsWalking;
    }
}
