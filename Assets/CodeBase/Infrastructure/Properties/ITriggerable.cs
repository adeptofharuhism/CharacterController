using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Properties
{
    public interface ITriggerable
    {
        void OnTriggerEnter(Collider collider);
        void OnTriggerExit(Collider collider);
    }
}
