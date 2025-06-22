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

        public void SetBallColor(BallColor color)
        {
            if (color == BallColor.None) return;
            ballColor = color;
            if (ballMeshRenderer != null && ballMaterials != null && (int)color - 1 < ballMaterials.Length)
            {
                ballMeshRenderer.material = ballMaterials[(int)color - 1];
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Collided with Player: " + other.gameObject.name);
                // Returns an object to the pool and deactivates it.
                ObjectPoolManager.Instance.DespawnObject(gameObject);
                if (PlayerBall.Instance.BallColor != ballColor)
                {
                    // End Game
                    Debug.Log($"Game Over! Ball color mismatch. {ballColor}");
                    PlayerBall.Instance.gameObject.SetActive(false);
                    GameManager.Instance.EndGame();
                }
                else
                {
                    // Add points
                    PointManager.Instance.AddPoints(1);
                    PlayerBall.Instance.PlayHitEffect();
                }
            }
        }
    }
}