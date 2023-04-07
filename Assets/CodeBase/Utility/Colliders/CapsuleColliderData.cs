using System;
using UnityEngine;

namespace Assets.CodeBase.Utility.Colliders
{
    [Serializable]
    public class CapsuleColliderData
    {
        [SerializeField] private CapsuleCollider _collider;
        
        private Vector3 _colliderCenterInLocalSpace;
        private Vector3 _colliderVerticalExtents;

        public CapsuleCollider Collider => _collider;
        public Vector3 ColliderCenterInLocalSpace => _colliderCenterInLocalSpace;
        public Vector3 ColliderVerticalExtents => _colliderVerticalExtents;

        public void Initialize() {
            UpdateColliderData();
        }

        public void UpdateColliderData() {
            _colliderCenterInLocalSpace = Collider.center;

            _colliderVerticalExtents = new Vector3(0f, _collider.bounds.extents.y, 0f);
        }
    }
}
