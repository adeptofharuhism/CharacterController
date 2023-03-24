﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.CodeBase.Character.States.Movement.Grounded
{
    public class IdlingState : MovementState
    {
        public IdlingState(MovementStateMachine stateMachine) : base(stateMachine) {
        }
    }
}
