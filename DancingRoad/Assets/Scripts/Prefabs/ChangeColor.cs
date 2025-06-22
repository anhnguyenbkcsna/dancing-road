using System.Net;
using UnityEngine;

namespace Assets.Scripts.Prefabs
{
    public class ChangeColor : MonoBehaviour
    {
        public BallColor color;
        [Tooltip("Aqua, Purple, Red, Yellow")]
        [SerializeField] private Color[] colors;
        [SerializeField] private MeshRenderer meshRenderer;

        private void Start()
        {
            ChangeAlbedoColor(color);
        }

        public void ChangeAlbedoColor(BallColor color)
        {
            this.color = color;
            if (meshRenderer != null && colors != null && (int)color - 1 < colors.Length)
            {
                meshRenderer.material.color = colors[(int)color - 1];
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("ChangeColor: collide with " + other.gameObject.name);
                PlayerBall.Instance.SetBallColor(color);
            }            
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeAlbedoColor(color);
            }
        }
#endif
    }
}