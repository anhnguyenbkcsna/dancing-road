using System;
using Assets.Scripts.Prefabs;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.Manager
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance { get; private set; }
        [Range(0, 50)] public int distance = 20;

        [Header("Object Pool Settings")]
        [SerializeField] private GameObject objectToPool;
        [SerializeField] private int initialPoolSize = 25;

        private ObjectPool<GameObject> objectPool;
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

        public void CreatePool(GameObject objectToPool, Vector3 position, Quaternion rotation)
        {
            if (Instance.objectPool == null)
            {
                Instance.objectPool = new ObjectPool<GameObject>(
                    () => CreateObject(objectToPool, position, rotation),
                    OnGetFromPool,
                    OnReleaseObject,
                    OnDestroyObject
                );
            }
            else
            {
                Debug.LogWarning("Object pool already initialized.");
            }
        }

        private GameObject CreateObject(GameObject objectToPool, Vector3 position, Quaternion rotation)
        {
            GameObject obj = Instantiate(objectToPool, position, rotation);
            obj.transform.SetParent(Instance.transform);
            obj.SetActive(false); // Keep inactive until needed
            return obj;
        }

        private void OnReleaseObject(GameObject obj)
        {
            if (Instance.objectPool != null)
            {
                obj.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Object pool is not initialized.");
            }
        }

        private void OnGetFromPool(GameObject obj)
        {
            obj.SetActive(true);
        }

        private void OnDestroyObject(GameObject obj)
        {
            if (Instance.objectPool != null)
            {
                Destroy(obj);
            }
            else
            {
                Debug.LogWarning("Object pool is not initialized.");
            }
        }

        public GameObject SpawnObject(BeatMapEvent data, Vector3 spawnPos)
        {
            var pooledObject = Instance.objectPool.Get();
            // var laneX = data.lane * 2;
            // var laneZ = data.tick * (60f / GameManager.Instance.BPM) * distance; // Calculate position based on BPM
            // var spawnPos = transform.position + new Vector3(laneX, 0, laneZ);
            pooledObject.transform.position = spawnPos;

            // Fix physics issuess
            var rb = pooledObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            var ballPoint = pooledObject.GetComponent<BallPoint>();
            if (ballPoint != null)
            {
                ballPoint.SetBallColor(data.color);
            }
            else
            {
                Debug.LogError("BallPoint component not found on the spawned ball prefab.");
            }
            pooledObject.SetActive(true);
            return pooledObject;
        }

        public void DespawnObject(GameObject obj)
        {
            if (Instance.objectPool != null)
            {
                Instance.objectPool.Release(obj);
                obj.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Object pool is not initialized.");
            }
        }
    }
}