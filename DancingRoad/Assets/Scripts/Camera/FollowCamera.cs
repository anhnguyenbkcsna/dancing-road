using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float smoothSpeed = 0.125f;
        private Vector3 _offset;

        void Awake()
        {
            _offset = transform.position - target.position;
        }

        void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, target.position + _offset, smoothSpeed);
        }

    }
}