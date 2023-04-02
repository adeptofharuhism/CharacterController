using System;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.States
{
    [Serializable]
    public class UnitRotationData
    {
        [SerializeField] private Vector3 _targetRotationReachTime;

        public Vector3 TargetRotationReachTime => _targetRotationReachTime;
    }
}
