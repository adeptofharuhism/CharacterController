using System;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.States.Grounded.Moving
{
    [Serializable]
    public class UnitSprintData
    {
        [SerializeField, Range(1f, 3f)]
        private float _speedModifier = 1.7f;
        [SerializeField, Range(0f, 5f)]
        private float _sprintToRunTime = 1f;

        public float SpeedModifier => _speedModifier;
        public float SprintToRunTime => _sprintToRunTime;
    }
}
