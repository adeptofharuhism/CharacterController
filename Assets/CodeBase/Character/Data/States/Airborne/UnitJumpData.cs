using System;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.States.Airborne
{
    [Serializable]
    public class UnitJumpData 
    {
        [SerializeField] private Vector3 _stationaryForce;
        [SerializeField] private Vector3 _weakForce;
        [SerializeField] private Vector3 _mediumForce;
        [SerializeField] private Vector3 _strongForce;
        [SerializeField] private UnitRotationData _rotationData;
        [SerializeField, Range(0f,5f)] private float _jumpToGroundRayDistance = 2f;
        [SerializeField, Range(0f,10f)] private float _decelerationForce = 1.5f;
        [SerializeField] private AnimationCurve _jumpForceModifierOnSlopeUpwards;
        [SerializeField] private AnimationCurve _jumpForceModifierOnSlopeDownwards;

        public Vector3 StationaryForce => _stationaryForce;
        public Vector3 WeakForce => _weakForce;
        public Vector3 MediumForce => _mediumForce;
        public Vector3 StrongForce => _strongForce;
        public UnitRotationData RotationData => _rotationData;
        public float JumpToGroundRayDistance => _jumpToGroundRayDistance;
        public float DecelerationForce => _decelerationForce;
        public AnimationCurve JumpForceModifierOnSlopeUpwards => _jumpForceModifierOnSlopeUpwards;
        public AnimationCurve JumpForceModifierOnSlopeDownwards => _jumpForceModifierOnSlopeDownwards;
    }
}
