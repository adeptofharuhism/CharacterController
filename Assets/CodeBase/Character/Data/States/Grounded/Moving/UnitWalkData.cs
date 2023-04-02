using System;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.States.Grounded.Moving
{
    [Serializable]
    public class UnitWalkData
    {
        [SerializeField] private float _speedModifier = 0.225f;

        public float SpeedModifier => _speedModifier;
    }
}
