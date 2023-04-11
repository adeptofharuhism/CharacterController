using Assets.CodeBase.Character.Animation;
using Assets.CodeBase.Character.Data.Colliders;
using Assets.CodeBase.Character.Data.Layers;
using Assets.CodeBase.Character.Data.States;
using Assets.CodeBase.Character.Data.States.Airborne;
using Assets.CodeBase.Character.Data.States.Grounded;
using Assets.CodeBase.Character.States.Movement.Airborne;
using Assets.CodeBase.Character.States.Movement.Grounded;
using Assets.CodeBase.Character.States.Movement.Grounded.Landing;
using Assets.CodeBase.Character.States.Movement.Grounded.Moving;
using Assets.CodeBase.Character.States.Movement.Grounded.Stopping;
using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Infrastructure.Services.Input;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement
{
    public class MovementStateMachine : UnitStateMachine
    {
        public MovementStateMachine(
            Transform unitTransform,
            IInputService inputService, 
            UnitGroundedData groundedData,
            UnitAirborneData airborneData, 
            Rigidbody rigidbody, 
            UnitCapsuleColliderUtility colliderUtility, 
            UnitLayerData layerData, 
            Animator animator,
            UnitAnimationData animationData) {

            MovementStateConstructionData constructionData = new MovementStateConstructionData {
                StateMachine = this,
                InputService = inputService,
                ReusableData = new UnitStateReusableData(),
                GroundedData = groundedData,
                AirborneData = airborneData,
                Rigidbody = rigidbody,
                ColliderUtility = colliderUtility,
                LayerData = layerData,
                Animator = animator,
                AnimationData = animationData
            };

            _states = new Dictionary<Type, IUnitExitableState>() {
                [typeof(IdlingState)] = new IdlingState(constructionData, unitTransform),

                [typeof(WalkingState)] = new WalkingState(constructionData, unitTransform),
                [typeof(RunningState)] = new RunningState(constructionData, unitTransform),
                [typeof(SprintingState)] = new SprintingState(constructionData, unitTransform),
                [typeof(DashingState)] = new DashingState(constructionData, unitTransform),

                [typeof(LightStoppingState)] = new LightStoppingState(constructionData, unitTransform),
                [typeof(MediumStoppingState)] = new MediumStoppingState(constructionData, unitTransform),
                [typeof(HardStoppingState)] = new HardStoppingState(constructionData, unitTransform),

                [typeof(LightLandingState)] = new LightLandingState(constructionData, unitTransform),
                [typeof(HardLandingState)] = new HardLandingState(constructionData, unitTransform),
                [typeof(RollingState)] = new RollingState(constructionData, unitTransform),

                [typeof(JumpingState)] = new JumpingState(constructionData, unitTransform),
                [typeof(FallingState)] = new FallingState(constructionData, unitTransform),
            };
        }
    }

    public class MovementStateConstructionData
    {
        public MovementStateMachine StateMachine { get; set; }
        public IInputService InputService { get; set; }
        public UnitStateReusableData ReusableData { get; set; }
        public UnitGroundedData GroundedData { get; set; }
        public UnitAirborneData AirborneData { get; set; }
        public Rigidbody Rigidbody { get; set; }
        public UnitCapsuleColliderUtility ColliderUtility { get; set; }
        public UnitLayerData LayerData { get; set; }
        public Animator Animator { get; set; }
        public UnitAnimationData AnimationData { get; set; }
    }
}
