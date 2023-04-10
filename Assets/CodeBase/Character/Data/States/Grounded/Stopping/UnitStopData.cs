using System;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.States.Grounded.Stopping
{
    [Serializable]
    public class UnitStopData
    {
        [SerializeField, Range(0f, 15f)] private float _lightDecelerationForce = 5f;
        [SerializeField, Range(0f, 15f)] private float _mediumDecelerationForce = 6.5f;
        [SerializeField, Range(0f, 15f)] private float _hardDecelerationForce = 5f;
        [SerializeField, Range(0f, 1f)] private float _forceStopStateExitTime = 0.14f;

        public float LightDecelerationForce => _lightDecelerationForce;
        public float MediumDecelerationForce => _mediumDecelerationForce;
        public float HardDecelerationForce => _hardDecelerationForce;
        public float ForceStopStateExitTime => _forceStopStateExitTime;
    }
}
