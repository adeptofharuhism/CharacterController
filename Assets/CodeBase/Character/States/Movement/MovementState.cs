using Assets.CodeBase.Character.Animation;
using Assets.CodeBase.Character.Data.Colliders;
using Assets.CodeBase.Character.Data.Layers;
using Assets.CodeBase.Character.Data.States;
using Assets.CodeBase.Character.Data.States.Airborne;
using Assets.CodeBase.Character.Data.States.Grounded;
using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement
{
    public class MovementState : IUnitState
    {
        protected readonly MovementStateMachine _stateMachine;
        protected readonly IInputService _inputService;
        protected readonly UnitStateReusableData _reusableData;
        protected readonly UnitGroundedData _groundedData;
        protected readonly UnitAirborneData _airborneData;
        protected readonly Rigidbody _rigidbody;
        protected readonly UnitCapsuleColliderUtility _colliderUtility;
        protected readonly UnitLayerData _layerData;
        protected readonly Animator _animator;
        protected readonly UnitAnimationData _animationData;

        public MovementState(MovementStateConstructionData constructionData) {
            _stateMachine = constructionData.StateMachine;
            _inputService = constructionData.InputService;
            _reusableData = constructionData.ReusableData;
            _groundedData = constructionData.GroundedData;
            _airborneData = constructionData.AirborneData;
            _rigidbody = constructionData.Rigidbody;
            _colliderUtility = constructionData.ColliderUtility;
            _layerData = constructionData.LayerData;
            _animator = constructionData.Animator;
            _animationData = constructionData.AnimationData;

            InitializeData();
        }

        public virtual void Enter() {
            Debug.Log($"State: {GetType().Name}");

            AddInputActionsCallbacks();
        }

        public virtual void Exit() =>
            RemoveInputActionsCallbacks();

        public virtual void HandleInput() =>
            ReadMovementInput();

        public virtual void Update() { }

        public virtual void PhysicsUpdate() =>
            Move();

        public virtual void OnAnimationEnterEvent() { }

        public virtual void OnAnimationExitEvent() { }

        public virtual void OnAnimationTransitEvent() { }

        public virtual void OnTriggerEnter(Collider collider) {
            if (_layerData.IsGroundLayer(collider.gameObject.layer))
                OnContactWithGround(collider);
        }

        public virtual void OnTriggerExit(Collider collider) {
            if (_layerData.IsGroundLayer(collider.gameObject.layer))
                OnLostContactWithGround(collider);
        }

        private void InitializeData() =>
            SetBaseRotationData();

        private void ReadMovementInput() =>
            _reusableData.MovementInput = _inputService.MoveInputValue;

        private void Move() {
            if (_reusableData.MovementInput == Vector2.zero || _reusableData.MovementSpeedModifier == 0f)
                return;

            Vector3 movementDirection = GetMovementDirection();

            float targetRotationYAngle = Rotate(movementDirection);
            Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

            float movementSpeed = GetMovementSpeed();

            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
            _rigidbody.AddForce(
                targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity,
                ForceMode.VelocityChange);
        }

        private float Rotate(Vector3 direction) {
            float directionAngle = UpdateTargetRotation(direction);

            RotateTowardsTargetRotation();

            return directionAngle;
        }

        private void UpdateTargetRotationData(float targetAngle) {
            _reusableData.CurrentTargetRotation.y = targetAngle;
            _reusableData.DampedTargetRotationPassedTime.y = 0f;
        }

        private static float GetDirectionAngle(Vector3 direction) {
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            if (directionAngle < 0f)
                directionAngle += 360f;

            return directionAngle;
        }

        protected void SetBaseRotationData() {
            _reusableData.RotationData = _groundedData.BaseRotationData;
            _reusableData.TimeToReachTargetRotation = _groundedData.BaseRotationData.TargetRotationReachTime;
        }

        protected float UpdateTargetRotation(Vector3 direction) {
            float directionAngle = GetDirectionAngle(direction);

            if (directionAngle != _reusableData.CurrentTargetRotation.y)
                UpdateTargetRotationData(directionAngle);

            return directionAngle;
        }

        protected Vector3 GetTargetRotationDirection(float targetYAngle) =>
            Quaternion.Euler(0f, targetYAngle, 0f) * Vector3.forward;

        protected void RotateTowardsTargetRotation() {
            float currentYAngle = _rigidbody.rotation.eulerAngles.y;

            if (currentYAngle == _reusableData.CurrentTargetRotation.y)
                return;

            float smoothedYAngle = Mathf.SmoothDampAngle(
                currentYAngle,
                _reusableData.CurrentTargetRotation.y,
                ref _reusableData.DampedTargetRotationCurrentVelocity.y,
                _reusableData.TimeToReachTargetRotation.y - _reusableData.DampedTargetRotationPassedTime.y);

            _reusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

            _rigidbody.MoveRotation(targetRotation);
        }

        protected Vector3 GetPlayerHorizontalVelocity() {
            Vector3 playerHorizontalVelocity = _rigidbody.velocity;
            playerHorizontalVelocity.y = 0;

            return playerHorizontalVelocity;
        }

        protected void DecelerateHorizontally() {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            _rigidbody.AddForce(
                -playerHorizontalVelocity * _reusableData.MovementDecelerationForce,
                ForceMode.Acceleration);
        }

        protected void DecelerateVertically() {
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();

            _rigidbody.AddForce(
                -playerVerticalVelocity * _reusableData.MovementDecelerationForce,
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
            new Vector3(0f, _rigidbody.velocity.y, 0f);

        protected float GetMovementSpeed(bool considerSlopes = true) =>
            considerSlopes
            ? _groundedData.BaseSpeed * _reusableData.MovementOnSlopesSpeedModifier * _reusableData.MovementSpeedModifier
            : _groundedData.BaseSpeed * _reusableData.MovementSpeedModifier;

        protected Vector3 GetMovementDirection() =>
            new Vector3(_reusableData.MovementInput.x, 0f, _reusableData.MovementInput.y);

        protected void ResetVelocity() =>
            _rigidbody.velocity = Vector3.zero;

        protected void ResetVertivalVelocity() =>
            _rigidbody.velocity = GetPlayerHorizontalVelocity();

        protected void StartAnimation(int animationHash) =>
            _animator.SetBool(animationHash, true);

        protected void StopAnimation(int animationHash) =>
            _animator.SetBool(animationHash, false);

        protected virtual void OnContactWithGround(Collider collider) { }

        protected virtual void OnLostContactWithGround(Collider collider) { }

        protected virtual void AddInputActionsCallbacks() =>
            _inputService.WalkToggleStarted += WalkToggle;

        protected virtual void RemoveInputActionsCallbacks() =>
            _inputService.WalkToggleStarted -= WalkToggle;

        protected virtual void WalkToggle() =>
            _reusableData.IsWalking = !_reusableData.IsWalking;
    }
}
