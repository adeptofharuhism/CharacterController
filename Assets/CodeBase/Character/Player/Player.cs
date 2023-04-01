using Assets.CodeBase.Character.States.Movement;
using Assets.CodeBase.Character.States.Movement.Grounded;
using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace Assets.CodeBase.Character.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private IInputService _inputService;
        private MovementStateMachine _movementStateMachine;
        private Transform _mainCameraTransform;

        public IInputService InputService => _inputService;
        public Rigidbody Rigidbody => _rigidbody;
        public Transform MainCameraTransform => _mainCameraTransform;

        private void Awake() {
            _inputService = AllServices.Container.Single<IInputService>();

            _mainCameraTransform = UnityEngine.Camera.main.transform;

            _movementStateMachine = new MovementStateMachine(this);
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
