using UnityEngine;

namespace Assets.Scripts.Prefabs
{
    public class PlayerBall : MonoBehaviour
    {
        public static PlayerBall Instance { get; private set; }
        public BallColor BallColor { get; private set; }

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
            // Ensure only one instance of PlayerBall exists
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            SetBallColor(BallColor.Red); // Default color
        }

        public void SetBallColor(BallColor color)
        {
            BallColor = color;
            ballMeshRenderer.material = ballMaterials[(int)color];
        }
    }
}