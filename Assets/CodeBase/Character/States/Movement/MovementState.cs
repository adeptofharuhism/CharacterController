using Assets.CodeBase.Infrastructure;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement
{
    public class MovementState : IUnitState
    {
        protected readonly MovementStateMachine _stateMachine;

        protected Vector2 _movementInput;

        protected float _baseSpeed = 5f;
        protected float _speedModifier = 1f;

        protected Vector3 _currentTargetRotation;
        protected Vector3 _timeToReachTargetRotation;
        protected Vector3 _dampedTargetRotationCurrentVelocity;
        protected Vector3 _dampedTargetRotationPassedTime;

        protected static bool _isWalking = false;

        public MovementState(MovementStateMachine stateMachine) {
            _stateMachine = stateMachine;

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

        private void InitializeData() {
            _timeToReachTargetRotation.y = 0.14f;
        }

        private void ReadMovementInput() {
            _movementInput = _stateMachine.Player.InputService.MoveInputValue;
        }

        private void Move() {
            if (_movementInput == Vector2.zero || _speedModifier == 0f)
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
            _currentTargetRotation.y = targetAngle;

            _dampedTargetRotationPassedTime.y = 0f;
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

            if (directionAngle != _currentTargetRotation.y)
                UpdateTargetRotationData(directionAngle);

            return directionAngle;
        }

        protected Vector3 GetTargetRotationDirection(float targetYAngle) =>
            Quaternion.Euler(0f, targetYAngle, 0f) * Vector3.forward;

        protected void RotateTowardsTargetRotation() {
            float currentYAngle = _stateMachine.Player.Rigidbody.rotation.y;

            if (currentYAngle == _currentTargetRotation.y)
                return;

            float smoothedYAngle = Mathf.SmoothDampAngle(
                currentYAngle,
                _currentTargetRotation.y,
                ref _dampedTargetRotationCurrentVelocity.y,
                _timeToReachTargetRotation.y - _dampedTargetRotationPassedTime.y);

            _dampedTargetRotationPassedTime.y += Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

            _stateMachine.Player.Rigidbody.MoveRotation(targetRotation);
        }

        protected Vector3 GetPlayerHorizontalVelocity() {
            Vector3 playerHorizontalVelocity = _stateMachine.Player.Rigidbody.velocity;
            playerHorizontalVelocity.y = 0;

            return playerHorizontalVelocity;
        }

        protected float GetMovementSpeed() =>
            _baseSpeed * _speedModifier;

        protected Vector3 GetMovementDirection() =>
            new Vector3(_movementInput.x, 0f, _movementInput.y);

        protected void ResetVelocity() =>
            _stateMachine.Player.Rigidbody.velocity = Vector3.zero;

        protected virtual void AddInputActionsCallbacks() {
            _stateMachine.Player.InputService.WalkToggleTriggered += WalkToggle;
        }

        protected virtual void RemoveInputActionsCallbacks() {
            _stateMachine.Player.InputService.WalkToggleTriggered -= WalkToggle;
        }

        protected virtual void WalkToggle() => 
            _isWalking = !_isWalking;
    }
}
