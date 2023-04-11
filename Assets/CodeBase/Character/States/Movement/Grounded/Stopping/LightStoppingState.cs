using Assets.CodeBase.Utility.Colliders;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Stopping
{
    public class LightStoppingState : StoppingState
    {
        public LightStoppingState(MovementStateConstructionData constructionData, Transform unitTransform) : 
            base(constructionData, unitTransform) {
        }

        public override void Enter() {
            base.Enter();

            _reusableData.MovementDecelerationForce =
                _groundedData.StopData.LightDecelerationForce;

            _reusableData.CurrentJumpForce = _airborneData.JumpData.WeakForce;
        }
    }
}
