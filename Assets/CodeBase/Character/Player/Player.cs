using Assets.CodeBase.Character.States.Movement;
using Assets.CodeBase.Character.States.Movement.Grounded;
using UnityEngine;

namespace Assets.CodeBase.Character.Player
{
    public class Player : MonoBehaviour
    {
        private MovementStateMachine _movementStateMachine;

        private void Awake() {
            _movementStateMachine = new MovementStateMachine();
        }

        private void Start() {
            _movementStateMachine.Enter<IdlingState>();
        }

        private void Update() {
            _movementStateMachine.HandleInput();
            _movementStateMachine.Update();
        }

        private void FixedUpdate() {
            _movementStateMachine.PhysicsUpdate();
        }
    }
}
