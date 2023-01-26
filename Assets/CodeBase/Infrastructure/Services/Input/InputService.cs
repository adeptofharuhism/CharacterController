using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Input
{
    public class InputService : IInputService
    {
        private Controls _controls;
        private bool _moveInputTriggered = false;

        public bool MoveInputTriggered => _moveInputTriggered;
        public Vector2 MoveInputValue => _controls.Character.Move.ReadValue<Vector2>();

        public InputService() {
            _controls = new Controls();
        }

        public void Enable() {
            _controls.Character.Move.started += _ => _moveInputTriggered = true;
            _controls.Character.Move.canceled += _ => _moveInputTriggered = false;
            _controls.Enable();
        }

        public void Disable() {
            _controls.Disable();
        }
    }
}
