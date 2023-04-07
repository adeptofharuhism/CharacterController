using System;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.Colliders
{
    [Serializable]
    public class UnitTriggerColliderData
    {
        [SerializeField] private BoxCollider _groundCheckCollider;

        public BoxCollider GroundCheckCollider => _groundCheckCollider;
    }
}
