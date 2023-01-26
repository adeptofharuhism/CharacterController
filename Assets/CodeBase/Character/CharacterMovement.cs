using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Services.Input;
using System;
using UnityEngine;

namespace Assets.CodeBase.Character
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _movespeed;


        private IInputService _inputService;
        private Vector2 _moveDirection = Vector2.zero;
        private float _currentMovespeedPart;

        public float CurrentMovespeedPart => _currentMovespeedPart;

        private void Awake() {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update() {
            _moveDirection = Vector2.zero;
            if (_inputService.MoveInputTriggered) {
                _moveDirection = _inputService.MoveInputValue;

                transform.forward = new Vector3(_moveDirection.x, 0, _moveDirection.y);
            }

            UpdateMovespeedPart();
        }

        private void FixedUpdate() {
            _rigidbody.MovePosition(transform.position + transform.forward * _movespeed * Time.deltaTime * _currentMovespeedPart);

        }

        private void UpdateMovespeedPart() {
            _currentMovespeedPart = _moveDirection.magnitude;
        }
    }
}