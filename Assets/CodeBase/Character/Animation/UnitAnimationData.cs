using System;
using UnityEngine;

namespace Assets.CodeBase.Character.Animation
{
    [Serializable]
    public class UnitAnimationData
    {
        [Header("State group parameter names")]
        [SerializeField] private string _groundedParameterName = "Grounded";
        [SerializeField] private string _movingParameterName = "Moving";
        [SerializeField] private string _stoppingParameterName = "Stopping";
        [SerializeField] private string _landingParameterName = "Landing";
        [SerializeField] private string _airborneParameterName = "Airborne";
        [Header("Grounded parameter names")]
        [SerializeField] private string _idleParameterName = "IsIdling";
        [SerializeField] private string _dashParameterName = "IsDashing";
        [SerializeField] private string _walkParameterName = "IsWalking";
        [SerializeField] private string _runParameterName = "IsRunning";
        [SerializeField] private string _sprintParameterName = "IsSprinting";
        [SerializeField] private string _mediumStopParameterName = "IsMediumStopping";
        [SerializeField] private string _hardStopParameterName = "IsHardStopping";
        [SerializeField] private string _rollParameterName = "IsRolling";
        [SerializeField] private string _hardLandParameterName = "IsHardLanding";
        [Header("Airborne parameter names")]
        [SerializeField] private string _fallParameterName = "IsFalling";

        private int _groundedParameterHash;
        private int _movingParameterHash;
        private int _stoppingParameterHash;
        private int _landingParameterHash;
        private int _airborneParameterHash;

        private int _idleParameterHash;
        private int _dashParameterHash;
        private int _walkParameterHash;
        private int _runParameterHash;
        private int _sprintParameterHash;
        private int _mediumStopParameterHash;
        private int _hardStopParameterHash;
        private int _rollParameterHash;
        private int _hardLandParameterHash;

        private int _fallParameterHash;

        public int GroundedParameterHash => _groundedParameterHash;
        public int MovingParameterHash => _movingParameterHash;
        public int StoppingParameterHash => _stoppingParameterHash;
        public int LandingParameterHash => _landingParameterHash;
        public int AirborneParameterHash => _airborneParameterHash;

        public int IdleParameterHash => _idleParameterHash;
        public int DashParameterHash => _dashParameterHash;
        public int WalkParameterHash => _walkParameterHash;
        public int RunParameterHash => _runParameterHash;
        public int SprintParameterHash => _sprintParameterHash;
        public int MediumStopParameterHash => _mediumStopParameterHash;
        public int HardStopParameterHash => _hardStopParameterHash;
        public int RollParameterHash => _rollParameterHash;
        public int HardLandParameterHash => _hardLandParameterHash;

        public int FallParameterHash => _fallParameterHash;

        public void Initialize() {
            _groundedParameterHash = Animator.StringToHash(_groundedParameterName);
            _movingParameterHash = Animator.StringToHash(_movingParameterName);
            _stoppingParameterHash = Animator.StringToHash(_stoppingParameterName);
            _landingParameterHash = Animator.StringToHash(_landingParameterName);
            _airborneParameterHash = Animator.StringToHash(_airborneParameterName);

            _idleParameterHash = Animator.StringToHash(_idleParameterName);
            _dashParameterHash = Animator.StringToHash(_dashParameterName);
            _walkParameterHash = Animator.StringToHash(_walkParameterName);
            _runParameterHash = Animator.StringToHash(_runParameterName);
            _sprintParameterHash = Animator.StringToHash(_sprintParameterName);
            _mediumStopParameterHash = Animator.StringToHash(_mediumStopParameterName);
            _hardStopParameterHash = Animator.StringToHash(_hardStopParameterName);
            _rollParameterHash = Animator.StringToHash(_rollParameterName);
            _hardLandParameterHash = Animator.StringToHash(_hardLandParameterName);

            _fallParameterHash = Animator.StringToHash(_fallParameterName);
        }
    }
}
