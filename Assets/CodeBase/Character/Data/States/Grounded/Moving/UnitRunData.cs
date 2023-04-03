using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.States.Grounded.Moving
{
    [Serializable]
    public class UnitRunData
    {
        [SerializeField] private float _speedModifier = 1f;
        [SerializeField] private float _runToWalkTime = 1f;

        public float SpeedModifier => _speedModifier;
        public float RunToWalkTime => _runToWalkTime;
    }
}
