using System;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.States.Airborne
{
    [Serializable]
    public class UnitFallData
    {
        [SerializeField, Range(1f,15f)] private float _fallSpeedLimit = 15f;
        [SerializeField, Range(1f,100f)] private float _minimalDistanceToFall = 3f;

        public float FallSpeedLimit => _fallSpeedLimit;
        public float MinimalDistanceAsHardFall => _minimalDistanceToFall;
    }
}
