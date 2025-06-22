using System.CodeDom.Compiler;
using Assets.Scripts.Prefabs;
using UnityEngine;
using Utils;

namespace Assets.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public BeatMapData BeatMap;

        [SerializeField] private GameObject tutorial;
        [SerializeField] private GameObject endGameUI;
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private GameObject changeColorPrefab;
        [SerializeField] private GameObject endGamePrefab;

        [Range(0, 50)] public int distance = 20;
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
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            LoadBeatMap();
            SpawnBalls();
        }

        private void LoadBeatMap()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("Beatmap/Unity_TheFatRat");
            if (jsonFile != null)
            {
                BeatMap = new BeatMapData();
                BeatMap.events = JsonUtility.FromJson<BeatMapData>(jsonFile.text).events;
                Debug.Log("Beatmap loaded successfully with " + BeatMap.events.Count + " events.");
                ObjectPoolManager.Instance.CreatePool(ballPrefab, Vector3.zero, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Beat map not found!");
            }
        }

        private void SpawnBalls()
        {
            if (BeatMap != null && BeatMap.events != null)
            {
                foreach (var beatEvent in BeatMap.events)
                {
                    var laneX = beatEvent.lane * 2;
                    var laneZ = beatEvent.tick * (60f / BPM) * distance ; // Calculate position based on BPM
                    var spawnPos = transform.position + new Vector3(laneX, 0, laneZ);
                    if (beatEvent.type == "change_color")
                    {
                        // Instantiate prefab
                        var newChangeColor = Instantiate(changeColorPrefab, spawnPos, Quaternion.Euler(-90, 0, 0));
            
                        // Set parent and match scale
                        newChangeColor.transform.SetParent(transform);
                        newChangeColor.transform.localScale = new Vector3(90, -90, 90);
                        newChangeColor.GetComponent<ChangeColor>().ChangeAlbedoColor(beatEvent.color);
                    }
                    else if (beatEvent.type == "ball")
                    {
                        GameObject ball = ObjectPoolManager.Instance.SpawnObject(beatEvent, spawnPos);
                    }
                    else if (beatEvent.type == "end")
                    {
                        // Instantiate end game flag prefab
                        var newChangeColor = Instantiate(endGamePrefab, spawnPos, Quaternion.identity);
            
                        // Set parent and match scale
                        newChangeColor.transform.SetParent(transform);
                        newChangeColor.GetComponent<ChangeColor>().ChangeAlbedoColor(beatEvent.color);
                    }
                }
            }
            else
            {
                Debug.LogWarning("No events found in the beat map.");
            }
        }

        // private void SpawnChangeColor()
        // {
        //     if (BeatMap != null && BeatMap.events != null)
        //     {
        //         foreach (var beatEvent in BeatMap.events)
        //         {
        //             if (beatEvent.type == "ChangeColor")
        //             {
        //                 var laneX = beatEvent.lane * 2;
        //                 var laneZ = beatEvent.tick * (60f / BPM) ; // Calculate position based on BPM
        //                 var spawnPos = transform.position + new Vector3(laneX, 0, laneZ);
        //                 var newChangeColor = Instantiate(changeColorPrefab, spawnPos, Quaternion.identity);
        //             }
        //         }
        //     }
        //     else
        //     {
        //         Debug.LogWarning("No events found in the beat map.");
        //     }
        // }

        public void EndTutorial()
        {
            AudioManager.Instance.PlayDefaultBGM();
            tutorial.gameObject.SetActive(false);
        }

        public void EndGame()
        {
            Debug.Log("GameManger: Ending game");
            endGameUI.SetActive(true);
            AudioManager.Instance.StopBGM();
        }
    }
}
