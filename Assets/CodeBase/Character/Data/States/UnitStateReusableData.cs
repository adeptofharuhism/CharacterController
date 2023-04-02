﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeBase.Character.Data.States
{
    public class UnitStateReusableData
    {
        private Vector3 _currentTargetRotation;
        private Vector3 _timeToReachTargetRotation;
        private Vector3 _dampedTargetRotationCurrentVelocity;
        private Vector3 _dampedTargetRotationPassedTime;

        public Vector2 MovementInput { get; set; }
        public float MovementSpeedModifier { get; set; } = 1f;
        public bool IsWalking{ get; set; }

        public ref Vector3 CurrentTargetRotation {
            get {
                return ref _currentTargetRotation;
            }
        }
        
        public ref Vector3 TimeToReachTargetRotation {
            get {
                return ref _timeToReachTargetRotation;
            }
        }

        public ref Vector3 DampedTargetRotationCurrentVelocity {
            get {
                return ref _dampedTargetRotationCurrentVelocity;
            }
        }

        public ref Vector3 DampedTargetRotationPassedTime {
            get {
                return ref _dampedTargetRotationPassedTime;
            }
        }
    }
}
