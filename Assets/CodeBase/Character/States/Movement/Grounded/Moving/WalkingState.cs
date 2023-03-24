﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Moving
{
    public class WalkingState : MovementState
    {
        public WalkingState(MovementStateMachine stateMachine) : base(stateMachine) {
        }
    }
}
