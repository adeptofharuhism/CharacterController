using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement
{
    public class MovementState : IUnitState
    {
        public virtual void Enter() {
            Debug.Log($"State: {GetType().Name}");
        }

        public virtual void Exit() {
            
        }

        public virtual void HandleInput() {
            
        }

        public virtual void PhysicsUpdate() {
            
        }

        public virtual void Update() {
            
        }
    }
}
