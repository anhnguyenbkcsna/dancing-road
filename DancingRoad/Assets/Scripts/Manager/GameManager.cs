using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public BeatMapData BeatMap;

        [SerializeField] private GameObject endGameUI;

        [Range(60f, 180f)]
        [SerializeField] private float bpm = 120f; // Beats per minute

        #region Getters and setters
        public float BPM
        {
            get => bpm;
        }

        #endregion

        private void Awake()
        {
            // Ensure only one instance of GameManager exists
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            // Make sure GameManager persists across scenes
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            LoadBeatMap();
        }

        void LoadBeatMap()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("Beatmap/Unity_TheFatRat");
            if (jsonFile != null)
            {
                BeatMap = new BeatMapData();
                BeatMap.events = JsonUtility.FromJson<Wrapper>(jsonFile.text).events;
                Debug.Log("Beatmap loaded successfully with " + BeatMap.events.Count + " events.");
            }
            else
            {
                Debug.LogError("Beat map not found!");
            }
        }

        public void EndGame()
        {
            Debug.Log("GameManger: Ending game");
            endGameUI.SetActive(true);
        }
    }
}
