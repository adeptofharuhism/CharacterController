using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.States.Grounded.Landing
{
    [Serializable]
    public class UnitRollData
    {
        [SerializeField, Range(0f, 3f)]
        private float _speedModifier = 1f;

        public float SpeedModifier => _speedModifier;
    }
}
