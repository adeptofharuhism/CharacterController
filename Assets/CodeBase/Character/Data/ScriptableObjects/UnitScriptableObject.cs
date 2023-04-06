using Assets.CodeBase.Character.Data.States.Airborne;
using Assets.CodeBase.Character.Data.States.Grounded;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Unit", menuName = "Custom/Characters/Unit")]
    public class UnitScriptableObject : ScriptableObject
    {
        [SerializeField] private UnitGroundedData _groundedData;
        [SerializeField] private UnitAirborneData _airborneData;

        public UnitGroundedData GroundedData => _groundedData;
        public UnitAirborneData AirborneData => _airborneData;
    }
}
