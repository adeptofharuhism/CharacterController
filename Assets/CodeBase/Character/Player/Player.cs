using Assets.CodeBase.Character.Data.Colliders;
using Assets.CodeBase.Character.Data.Layers;
using Assets.CodeBase.Character.Data.ScriptableObjects;
using Assets.CodeBase.Character.States.Movement;
using Assets.CodeBase.Character.States.Movement.Grounded;
using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Services.Input;
using Assets.CodeBase.Utility.Colliders;
using UnityEngine;

namespace Assets.CodeBase.Character.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField, Header("Movement Data")]
        private UnitScriptableObject _data;

        [SerializeField, Header("Collisions")]
        private UnitCapsuleColliderUtility _colliderUtility;
        [SerializeField] private UnitLayerData _layerData;

        private IInputService _inputService;
        private MovementStateMachine _movementStateMachine;
        private Transform _mainCameraTransform;

        public IInputService InputService => _inputService;
        public Rigidbody Rigidbody => _rigidbody;
        public Transform MainCameraTransform => _mainCameraTransform;
        public UnitScriptableObject Data => _data;
        public UnitCapsuleColliderUtility ColliderUtility => _colliderUtility;
        public UnitLayerData LayerData => _layerData;

        private void Awake() {
            _inputService = AllServices.Container.Single<IInputService>();

            _mainCameraTransform = UnityEngine.Camera.main.transform;

            _colliderUtility.Initialize();
            _colliderUtility.CalculateCapsuleColliderDimensions();

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

        private void OnTriggerEnter(Collider collider) => _movementStateMachine.OnTriggerEnter(collider);

        private void OnTriggerExit(Collider collider) => _movementStateMachine.OnTriggerExit(collider);

        private void OnValidate() {
            _colliderUtility.Initialize();
            _colliderUtility.CalculateCapsuleColliderDimensions();
        }
    }
}
