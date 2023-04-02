using Assets.CodeBase.Character.Data.States.Grounded;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Unit", menuName = "Custom/Characters/Unit")]
    public class UnitScriptableObject : ScriptableObject
    {
        [SerializeField] private UnitGroundedData _groundedData;

        public UnitGroundedData GroundedData => _groundedData;
    }
}
