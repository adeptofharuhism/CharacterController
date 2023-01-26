using UnityEngine;

namespace Assets.CodeBase.Character.Animation
{
    public class StateReaderLocator : MonoBehaviour
    {
        [SerializeField] private CharacterAnimator _stateReader;

        public IAnimationStateReader StateReader => _stateReader;
    }
}