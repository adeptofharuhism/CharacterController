using Assets.CodeBase.Character.Animation;
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
        [Header("Movement Data")]
        [SerializeField] private UnitScriptableObject _data;

        [Header("Collisions")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private UnitCapsuleColliderUtility _colliderUtility;
        [SerializeField] private UnitLayerData _layerData;

        [Header("Animations")]
        [SerializeField] private UnitAnimationData _animationData;
        [SerializeField] private Animator _animator;

        private IInputService _inputService;
        private MovementStateMachine _movementStateMachine;
        private Transform _mainCameraTransform;

        public IInputService InputService => _inputService;
        public Rigidbody Rigidbody => _rigidbody;
        //public Transform MainCameraTransform => _mainCameraTransform;
        public UnitScriptableObject Data => _data;
        public UnitCapsuleColliderUtility ColliderUtility => _colliderUtility;
        public UnitLayerData LayerData => _layerData;
        public UnitAnimationData AnimationData => _animationData;
        public Animator Animator => _animator;

        private void Awake() {
            _inputService = AllServices.Container.Single<IInputService>();

            _mainCameraTransform = UnityEngine.Camera.main.transform;

            _colliderUtility.Initialize();
            _colliderUtility.CalculateCapsuleColliderDimensions();

            _animationData.Initialize();

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

        public void OnAnimationEnterEvent() => _movementStateMachine.OnAnimationEnterEvent();

        public void OnAnimationExitEvent() => _movementStateMachine.OnAnimationExitEvent();

        public void OnAnimationTransitEvent() => _movementStateMachine.OnAnimationTransitEvent();
    }
}
