using System;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.Colliders
{
    [Serializable]
    public class UnitTriggerColliderData
    {
        [SerializeField] private BoxCollider _groundCheckCollider;
        private Vector3 _groundCheckColliderExtents;

        public BoxCollider GroundCheckCollider => _groundCheckCollider;

        public Vector3 GroundCheckColliderExtents => _groundCheckColliderExtents;

        public void Initialize() {
            _groundCheckColliderExtents = _groundCheckCollider.bounds.extents;
        }
    }
}
