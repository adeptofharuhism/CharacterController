using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 MoveInputValue { get; }
        bool MoveInputTriggered { get; }

        void Disable();
        void Enable();
    }
}