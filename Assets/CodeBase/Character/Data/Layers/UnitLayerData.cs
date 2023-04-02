using System;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.Layers
{
    [Serializable]
    public class UnitLayerData
    {
        [SerializeField] private LayerMask _groundLayer;

        public LayerMask GroundLayer => _groundLayer;
    }
}
