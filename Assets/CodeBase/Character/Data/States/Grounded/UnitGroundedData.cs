﻿using Assets.CodeBase.Character.Data.States.Grounded.Landing;
using Assets.CodeBase.Character.Data.States.Grounded.Moving;
using Assets.CodeBase.Character.Data.States.Grounded.Stopping;
using System;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.States.Grounded
{
    [Serializable]
    public class UnitGroundedData
    {
        [SerializeField] private float _baseSpeed = 5f;
        [SerializeField] private float _groundToFallRayDistance = 1f;
        [SerializeField] private UnitRotationData _baseRotationData;
        [SerializeField] private UnitWalkData _walkData;
        [SerializeField] private UnitRunData _runData;
        [SerializeField] private UnitDashData _dashData;
        [SerializeField] private UnitSprintData _sprintData;
        [SerializeField] private AnimationCurve _slopeSpeedAngles;
        [SerializeField] private UnitStopData _stopData;
        [SerializeField] private UnitRollData _rollData;

        public float BaseSpeed => _baseSpeed;
        public float GroundToFallRayDistance => _groundToFallRayDistance;
        public UnitRotationData BaseRotationData => _baseRotationData;
        public UnitWalkData WalkData => _walkData;
        public UnitRunData RunData => _runData;
        public UnitDashData DashData => _dashData;
        public UnitSprintData SprintData => _sprintData;
        public AnimationCurve SlopeSpeedAngles => _slopeSpeedAngles;
        public UnitStopData StopData => _stopData;
        public UnitRollData RollData => _rollData;
    }
}
