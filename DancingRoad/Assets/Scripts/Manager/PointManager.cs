using DG.Tweening;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Manager
{

    public class PointManager : MonoBehaviour
    {
        public static PointManager Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI pointText;
        [SerializeField] private TextMeshProUGUI streakText;
        [SerializeField] private Image perfectImage;
        [SerializeField] private Image fantasticImage;

        private int _streakCount;
        private int _points;

        private void Awake()
        {
            // Ensure only one instance of PointManager exists
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void AddPoints(int points)
        {
            _points += points;
            _streakCount += points;
            UpdateUI();
        }

        private void UpdateUI()
        {
            pointText.text = _points.ToString();
            streakText.text = "Streak: x" + _streakCount;
            streakText.gameObject.transform.DOScale((1 + 0.05f * _streakCount) * Vector3.one, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => {
                streakText.gameObject.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
            });
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                AddPoints(1); // For testing purposes, add 10 points when 'P' is pressed
                Debug.Log("Points added: 1");
            }
        }
#endif
    }
}
