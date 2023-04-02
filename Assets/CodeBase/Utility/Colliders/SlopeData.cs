using System;
using UnityEngine;

namespace Assets.CodeBase.Utility.Colliders
{
    [Serializable]
    public class SlopeData
    {
        [SerializeField, Range(0f,1f)] 
        private float _stepHeightPercentage = 0.25f;
        [SerializeField, Range(0f, 5f)]
        private float _floatRayDistance = 2f;
        [SerializeField, Range(0f, 50f)]
        private float _stepReachForce = 25f;

        public float StepHeightPercentage => _stepHeightPercentage;
        public float FloatRayDistance => _floatRayDistance;
        public float StepReachForce => _stepReachForce;
    }
}
