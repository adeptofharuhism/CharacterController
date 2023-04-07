using Assets.CodeBase.Utility.Colliders;
using System;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.Colliders
{
    [Serializable]
    public class UnitCapsuleColliderUtility : CapsuleColliderUtility
    {
        [SerializeField] private UnitTriggerColliderData _triggerColliderData;

        public UnitTriggerColliderData TriggerColliderData => _triggerColliderData;
    }
}
