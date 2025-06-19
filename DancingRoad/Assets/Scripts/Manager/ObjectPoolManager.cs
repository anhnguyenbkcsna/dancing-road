using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.Manager
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance { get; private set; }

        [Header("Object Pool Settings")]
        [SerializeField] private GameObject objectToPool;
        [SerializeField] private int initialPoolSize = 10;

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

        private static void CreatePool(GameObject objectToPool, Vector3 position, Quaternion rotation)
        {
            if (Instance.objectPool == null)
            {
                Instance.objectPool = new ObjectPool<GameObject>(
                    () => CreateObject(objectToPool, position, rotation),
                    OnReleaseObject,
                    OnDestroyObject
                );
                // Prepopulate the pool with initial objects
                for (int i = 0; i < Instance.initialPoolSize; i++)
                {
                    Instance.objectPool.Get();
                }
            }
            else
            {
                Debug.LogWarning("Object pool already initialized.");
            }
        }

        private static GameObject CreateObject(GameObject objectToPool, Vector3 position, Quaternion rotation)
        {
            objectToPool.SetActive(false);
            GameObject obj = Instantiate(objectToPool, position, rotation);
            obj.SetActive(true);
            objectToPool.SetActive(true);

            obj.transform.SetParent(Instance.transform); // Set parent to ObjectPoolManager
            return null;
        }

        private static void OnReleaseObject(GameObject obj)
        {
            if (Instance.objectPool != null)
            {
                Instance.objectPool.Release(obj);
            }
            else
            {
                Debug.LogWarning("Object pool is not initialized.");
            }
        }

        private static void OnDestroyObject(GameObject obj)
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

        public static GameObject SpawnObject(GameObject objectToPool, Vector3 position, Quaternion rotation)
        {
            if (Instance.objectPool == null)
            {
                CreatePool(objectToPool, position, rotation);
            }
            GameObject pooledObject = Instance.objectPool.Get();
            pooledObject.transform.position = position;
            pooledObject.transform.rotation = rotation;
            pooledObject.SetActive(true);
            return pooledObject;
        }

        public static void DespawnObject(GameObject obj)
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