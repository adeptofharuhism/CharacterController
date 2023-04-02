using System;
using UnityEngine;

namespace Assets.CodeBase.Utility.Colliders
{
    [Serializable]
    public class CapsuleColliderData
    {
        [SerializeField] private CapsuleCollider _collider;
        
        private Vector3 _colliderCenterInLocalSpace;

        public CapsuleCollider Collider => _collider;
        public Vector3 ColliderCenterInLocalSpace => _colliderCenterInLocalSpace;

        public void Initialize() {
            UpdateColliderData();
        }

        public void UpdateColliderData() {
            _colliderCenterInLocalSpace = Collider.center;
        }
    }
}
