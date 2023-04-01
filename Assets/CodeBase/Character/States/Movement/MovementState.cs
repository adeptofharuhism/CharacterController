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

        public MovementState(MovementStateMachine stateMachine) {
            _stateMachine = stateMachine;
        }

        public virtual void Enter() {
            Debug.Log($"State: {GetType().Name}");
        }

        public virtual void Exit() {

        }

        public virtual void HandleInput() {
            ReadMovementInput();
        }

        public virtual void Update() {

        }

        public virtual void PhysicsUpdate() {
            Move();
        }

        private void ReadMovementInput() {
            _movementInput = _stateMachine.Player.InputService.MoveInputValue;
        }

        private void Move() {
            if (_movementInput == Vector2.zero || _speedModifier == 0f)
                return;

            Vector3 movementDirection = GetMovementDirection();
            float movementSpeed = GetMovementSpeed();

            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
            _stateMachine.Player.Rigidbody.AddForce(
                movementDirection * movementSpeed - currentPlayerHorizontalVelocity,
                ForceMode.VelocityChange);
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
    }
}
