using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.States.Airborne
{
    [Serializable]
    public class UnitFallData
    {
        [SerializeField, Range(1f,15f)] private float _fallSpeedLimit = 15f;

        public float FallSpeedLimit => _fallSpeedLimit;
    }
}
