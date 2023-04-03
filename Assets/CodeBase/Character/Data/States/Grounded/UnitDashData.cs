using System;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.States.Grounded
{
    [Serializable]
    public class UnitDashData
    {
        [SerializeField, Range(1f, 3f)]
        private float _speedModifier = 2f;
        [SerializeField, Range(0f, 2f)]
        private float _consecutiveTime = 1f;
        [SerializeField, Range(1, 10)]
        private int _consecutiveDashesLimitAmount = 2;
        [SerializeField, Range(0f, 5f)]
        private float _dashLimitReachedCooldown = 0.75f;

        public float SpeedModifier => _speedModifier;
        public float ConsecutiveTime => _consecutiveTime;
        public int ConsecutiveDashesLimitAmount => _consecutiveDashesLimitAmount;
        public float DashLimitReachedCooldown => _dashLimitReachedCooldown;
    }
}