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
            var targetPosition = new Vector3(0.5f * target.position.x + _offset.x, 
                                              target.position.y + _offset.y, 
                                              target.position.z + _offset.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }

    }
}