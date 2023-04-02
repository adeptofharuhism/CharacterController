using System;
using UnityEngine;

namespace Assets.CodeBase.Utility.Colliders
{
    [Serializable]
    public class DefaultColliderData
    {
        [SerializeField] private float _height = 1.8f;
        [SerializeField] private float _centerY = 0.9f;
        [SerializeField] private float _radius = 0.2f;

        public float Height => _height;
        public float CenterY => _centerY;
        public float Radius => _radius;
    }
}
