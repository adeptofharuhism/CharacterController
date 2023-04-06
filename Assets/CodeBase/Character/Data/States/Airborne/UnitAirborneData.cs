using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.States.Airborne
{
    [Serializable]
    public class UnitAirborneData
    {
        [SerializeField] private UnitJumpData _jumpData;

        public UnitJumpData JumpData => _jumpData;
    }
}
