using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Input
{
    public class InputService : IInputService
    {
        public bool MoveInputTriggered => _moveInputTriggered;
        public Vector2 MoveInputValue => _controls.Character.Move.ReadValue<Vector2>();

        public event IInputService.EventZeroParameters WalkToggleTriggered;
        public event IInputService.EventZeroParameters MovementStarted;
        public event IInputService.EventZeroParameters MovementPerformed;
        public event IInputService.EventZeroParameters MovementCancelled;
        public event IInputService.EventZeroParameters DashStarted;
        public event IInputService.EventZeroParameters SprintPerformed;

        private Controls _controls;
        private bool _moveInputTriggered = false;

        private float _timeByWhichDashDisabled = 0; 

        public InputService() {
            _controls = new Controls();
        }

        public void Initialize() {
            _controls.Character.Move.started += _ => _moveInputTriggered = true;
            _controls.Character.Move.canceled += _ => _moveInputTriggered = false;

            _controls.Character.Move.started += _ => MovementStarted?.Invoke();
            _controls.Character.Move.performed += _ => MovementPerformed?.Invoke();
            _controls.Character.Move.canceled += _ => MovementCancelled?.Invoke();

            _controls.Character.Dash.started += _ => HandleDashStarted();

            _controls.Character.Sprint.performed += _ =>SprintPerformed?.Invoke();

            _controls.Character.WalkToggle.started += _ => WalkToggleTriggered?.Invoke();
        }

        public void Enable() => _controls.Enable();

        public void Disable() => _controls.Disable();

        public void DisableDashFor(float seconds) => 
            _timeByWhichDashDisabled = Time.time + seconds;

        private void HandleDashStarted() {
            if (Time.time > _timeByWhichDashDisabled)
                DashStarted?.Invoke();
        }
    }
}
