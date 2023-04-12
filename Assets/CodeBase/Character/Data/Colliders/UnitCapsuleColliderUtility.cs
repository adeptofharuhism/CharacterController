using Assets.CodeBase.Utility.Colliders;
using System;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.Colliders
{
    [Serializable]
    public class UnitCapsuleColliderUtility : CapsuleColliderUtility
    {
        [SerializeField] private UnitTriggerColliderData _triggerColliderData;

        public UnitTriggerColliderData TriggerColliderData => _triggerColliderData;

        protected override void OnInitialize() {
            _triggerColliderData.Initialize();
            CalculateBoxTriggerHeight();
        }
        
        private void CalculateBoxTriggerHeight() {
            float triggerHeight = DefaultColliderData.Height * SlopeData.StepHeightPercentage;

            BoxCollider groundCheckCollider = _triggerColliderData.GroundCheckCollider;
            groundCheckCollider.size = new Vector3(
                groundCheckCollider.size.x,
                triggerHeight,
                groundCheckCollider.size.z);
            groundCheckCollider.center = new Vector3(
                groundCheckCollider.center.x,
                triggerHeight/2,
                groundCheckCollider.center.z);
        }
    }
}
