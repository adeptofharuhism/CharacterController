using UnityEngine;

namespace Assets.CodeBase.Character.Camera
{
    public class CharacterCameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _targetLookOffset;
        [SerializeField] private Vector3 _followerPositionOffset;

        private void LateUpdate() {
            transform.position = _target.position + _followerPositionOffset;
            transform.LookAt(_target.position + _targetLookOffset);
        }
    }
}