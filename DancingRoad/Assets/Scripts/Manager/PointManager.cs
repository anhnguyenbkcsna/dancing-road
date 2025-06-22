using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class PointManager : MonoBehaviour
    {
        public static PointManager Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI pointText;
        [SerializeField] private TextMeshProUGUI streakText;
        [SerializeField] private GameObject perfectImage;
        [SerializeField] private GameObject fantasticImage;

        [Header("Time")] [Range(0, 3f)] public float zoomFrom = 1.5f;
        [Range(0, 3f)] public float zoomTo = 1.0f;
        [Range(0, 3f)] public float duration = 0.5f;


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
            var rnd = Random.Range(0, 100);
            if (rnd < 30)
            {
                ShowPerfectImage();
            }
            else if (rnd > 70)
            {
                ShowFantasticImage();
            }
        }

        private void UpdateUI()
        {
            pointText.text = _points.ToString();
            // streakText.gameObject.SetActive(true);
            // streakText.text = "Streak: x" + _streakCount;
            pointText.gameObject.transform.DOScale((1 + 0.35f) * Vector3.one, 0.2f)
                .SetEase(Ease.OutBounce).OnComplete(() =>
                {
                    pointText.gameObject.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBounce);
                });
        }

        private void ShowPerfectImage()
        {
            perfectImage.transform.localScale = zoomFrom * Vector3.one;
            perfectImage.gameObject.SetActive(true);
            
            perfectImage.transform.DOScale(zoomTo, duration).SetEase(Ease.OutBack).OnComplete(
                () => perfectImage.gameObject.SetActive(false)
            );
        }

        private void ShowFantasticImage()
        {
            fantasticImage.transform.localScale = zoomFrom * Vector3.one;
            fantasticImage.gameObject.SetActive(true);
            
            fantasticImage.transform.DOScale(zoomTo, duration).SetEase(Ease.OutBack).OnComplete(
                () => fantasticImage.gameObject.SetActive(false)
            );
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