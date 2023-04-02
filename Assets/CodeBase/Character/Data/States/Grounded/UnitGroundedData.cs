using Assets.CodeBase.Character.Data.States.Grounded.Moving;
using System;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.States.Grounded
{
    [Serializable]
    public class UnitGroundedData
    {
        [SerializeField] private float _baseSpeed = 5f;
        [SerializeField] private UnitRotationData _baseRotationData;
        [SerializeField] private UnitWalkData _walkData;
        [SerializeField] private UnitRunData _runData;
        [SerializeField] private AnimationCurve _slopeSpeedAngles;

        public float BaseSpeed => _baseSpeed;
        public UnitRotationData BaseRotationData => _baseRotationData;
        public UnitWalkData WalkData => _walkData;
        public UnitRunData RunData => _runData;
        public AnimationCurve SlopeSpeedAngles => _slopeSpeedAngles;
    }
}
