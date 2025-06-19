using Assets.Scripts.Manager;
using UnityEngine;

namespace Assets.Scripts.Prefabs
{
    public class BallPoint : MonoBehaviour
    {
        public BallColor ballColor;
        [SerializeField] private MeshRenderer ballMeshRenderer;

        [Tooltip("Aqua, Purple, Red, Yellow")]
        [SerializeField] private Material[] ballMaterials;

        

        private void OnValidate()
        {
            if (ballMeshRenderer == null)
            {
                ballMeshRenderer = GetComponent<MeshRenderer>();
            }
        }

        private void Start()
        {
            SetBallColor(ballColor);
        }

        public void SetBallColor(BallColor color)
        {
            ballColor = color;
            if (ballMeshRenderer != null && ballMaterials != null && (int)color < ballMaterials.Length)
            {
                ballMeshRenderer.material = ballMaterials[(int)color];
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // Handle collision with Player
                Debug.Log("Collided with Player: " + other.gameObject.name);
                Destroy(gameObject);
                if (PlayerBall.Instance.BallColor != ballColor)
                {
                    // End Game
                    Debug.Log("Game Over! Ball color mismatch.");
                    PlayerBall.Instance.gameObject.SetActive(false);
                    GameManager.Instance.EndGame();
                }
                else
                {
                    // Add points
                    PointManager.Instance.AddPoints(1);
                }
            }
        }
    }
}