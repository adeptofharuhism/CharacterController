using System;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.Layers
{
    [Serializable]
    public class UnitLayerData
    {
        [SerializeField] private LayerMask _groundLayer;

        public LayerMask GroundLayer => _groundLayer;

        public bool ContainsLayer(LayerMask layerMask, int layer) =>
            (1 << layer & layerMask) != 0;

        public bool IsGroundLayer(int layer) =>
            ContainsLayer(GroundLayer, layer);
    }
}
