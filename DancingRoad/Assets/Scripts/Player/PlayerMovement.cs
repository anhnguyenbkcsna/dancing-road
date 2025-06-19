using Assets.Scripts.Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// Controls player movement based on input and BPM-based forward motion.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [Header("Input Settings")]
        [SerializeField] private InputActionReference moveAction;

        [Header("Movement Settings")]
        [SerializeField, Range(0f, 10f)] private float boundaries = 5f;
        [SerializeField, Range(0f, 10f)] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 5f;

        private UnityEngine.Camera mainCamera;
        private float bpmSpeed;

        #region Unity Methods

        private void Start()
        {
            mainCamera = UnityEngine.Camera.main;
            CalculateBpmSpeed();
        }

        private void Update()
        {
            HandleMovement();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Calculates forward movement speed based on game BPM.
        /// </summary>
        private void CalculateBpmSpeed()
        {
            float bpm = GameManager.Instance.BPM;
            bpmSpeed = bpm / 60f * 5f;
        }

        /// <summary>
        /// Handles player input and movement including forward motion and boundary clamping.
        /// </summary>
        private void HandleMovement()
        {
            Vector2 input = moveAction.action.ReadValue<Vector2>();

            // Apply movement input
            Vector3 moveVector = new Vector3(input.x, 0, 0);
            transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);

            // Apply BPM-based forward movement
            transform.Translate(Vector3.forward * bpmSpeed * Time.deltaTime, Space.World);

            // Clamp position within horizontal boundaries
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, -boundaries, boundaries);
            clampedPosition.y = 0f; // Lock Y position to ground
            transform.position = clampedPosition;
        }

        #endregion
    }
}
