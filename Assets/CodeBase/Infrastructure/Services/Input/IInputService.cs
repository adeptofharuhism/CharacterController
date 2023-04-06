using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 MoveInputValue { get; }
        bool MoveInputTriggered { get; }

        delegate void EventZeroParameters();
        event EventZeroParameters WalkToggleTriggered;
        event EventZeroParameters MovementCancelled;
        event EventZeroParameters DashStarted;
        event EventZeroParameters SprintPerformed;
        event EventZeroParameters MovementStarted;
        event EventZeroParameters MovementPerformed;
        event EventZeroParameters JumpStarted;

        void Disable();
        void DisableDashFor(float seconds);
        void Enable();
        void Initialize();
    }
}