using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeBase.Character.Animation
{
    public class AnimationEventTrigger : MonoBehaviour
    {
        [SerializeField] private Player.Player _player;

        public void TriggerOnAnimationEnter() {
            if (IsInAnimationTransition())
                return;

            _player.OnAnimationEnterEvent();
        }

        public void TriggerOnAnimationExit() {
            if (IsInAnimationTransition())
                return;

            _player.OnAnimationExitEvent();
        }

        public void TriggerOnAnimationTransit() {
            if (IsInAnimationTransition())
                return;

            _player.OnAnimationTransitEvent();
        }

        public void TriggerOnAnimationTransitForce() => 
            _player.OnAnimationTransitEvent();

        private bool IsInAnimationTransition(int layerIndex = 0) {
            return _player.Animator.IsInTransition(layerIndex);
        }
    }
}
