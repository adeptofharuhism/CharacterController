using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Moving
{
    public class WalkingState : MovingState
    {
        public WalkingState(MovementStateMachine stateMachine) : base(stateMachine) {
        }

        public override void Enter() {
            base.Enter();

            _stateMachine.ReusableData.MovementSpeedModifier = _groundedData.WalkData.SpeedModifier;
        }

        protected override void WalkToggle() {
            base.WalkToggle();

            _stateMachine.Enter<RunningState>();
        }
    }
}
