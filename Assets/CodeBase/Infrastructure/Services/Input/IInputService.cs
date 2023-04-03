using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 MoveInputValue { get; }
        bool MoveInputTriggered { get; }

        event InputService.EventZeroParameters WalkToggleTriggered;
        event InputService.EventZeroParameters MovementCancelled;
        event InputService.EventZeroParameters DashStarted;

        void Disable();
        void DisableDashFor(float seconds);
        void Enable();
    }
}