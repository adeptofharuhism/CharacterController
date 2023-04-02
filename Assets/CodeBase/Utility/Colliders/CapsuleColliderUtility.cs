using System;
using UnityEngine;

namespace Assets.CodeBase.Utility.Colliders
{
    [Serializable]
    public class CapsuleColliderUtility
    {
        [SerializeField] private CapsuleColliderData _capsuleColliderData;
        [SerializeField] private DefaultColliderData _defaultColliderData;
        [SerializeField] private SlopeData _slopeData;

        public CapsuleColliderData CapsuleColliderData => _capsuleColliderData;
        public DefaultColliderData DefaultColliderData => _defaultColliderData;
        public SlopeData SlopeData => _slopeData;

        public void Initialize() {

            _capsuleColliderData.Initialize();
        }

        public void CalculateCapsuleColliderDimensions() {
            SetCapsuleColliderRadius(_defaultColliderData.Radius);

            SetCapsuleColliderHeight(_defaultColliderData.Height * (1 - _slopeData.StepHeightPercentage));

            RecalculateCapsuleColliderCenter();

            RecalculateSmallHeightCenter();
        }

        private void SetCapsuleColliderRadius(float radius) =>
            _capsuleColliderData.Collider.radius = radius;

        private void SetCapsuleColliderHeight(float height) =>
            _capsuleColliderData.Collider.height = height;

        private void RecalculateCapsuleColliderCenter() {
            float colliderHeightDifference = _defaultColliderData.Height - _capsuleColliderData.Collider.height;

            Vector3 newColliderCenter = new Vector3(0f, _defaultColliderData.CenterY + colliderHeightDifference / 2, 0f);

            _capsuleColliderData.Collider.center = newColliderCenter;
        }

        private void RecalculateSmallHeightCenter() {
            float halfColliderHeight = _capsuleColliderData.Collider.height / 2f;
            if (halfColliderHeight < _capsuleColliderData.Collider.radius) {
                SetCapsuleColliderRadius(halfColliderHeight);
            }
            _capsuleColliderData.UpdateColliderData();
        }
    }
}
