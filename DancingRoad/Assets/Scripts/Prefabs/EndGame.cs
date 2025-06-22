using Assets.Scripts.Manager;
using UnityEngine;

namespace Assets.Scripts.Prefabs
{
    public class EndGame : MonoBehaviour
    {

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Collided with Player: " + other.gameObject.name);
                // End Game
                PlayerBall.Instance.gameObject.SetActive(false);
                GameManager.Instance.EndGame();
            }
        }
    }
}