using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public bool IsInitialized { get; private set; } = false;
        public Dictionary<PoolableType, Queue<GameObject>> poolDictionary = new Dictionary<PoolableType, Queue<GameObject>>();
        [SerializeField] private ObjectPoolData _poolData;

        public void Awake() {

            if(_poolData == null) {
            Debug.LogWarning("Pool data is null");
            }
            InitializePools();
        }

        private void InitializePools()
        {
            foreach(var pool in _poolData.pool) {
                Queue<GameObject> queue = new Queue<GameObject>(pool.Size);

                for(int i = 0 ; i<pool.Size; i++) {
                    GameObject gameObject = Instantiate(pool.Prefab,transform);
                    gameObject.SetActive(false);
                    queue.Enqueue(gameObject);
                }
                poolDictionary.Add(pool.Type,queue);
            }
            IsInitialized = true;
        }
        public GameObject SpawnFromPool(PoolableType itemType)
        {
            if (!poolDictionary.ContainsKey(itemType))
            {
                Debug.LogWarning("Pool with item " + itemType + " doesn't exist.");
                return null;
            }

            if (poolDictionary[itemType].Count == 0)
            {
                Debug.LogWarning("Pool with item " + itemType + " is empty.");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[itemType].Dequeue();
            objectToSpawn.SetActive(true);

            return objectToSpawn;
        }
        public void ReturnToPool(PoolableType type, GameObject gameObjectToReturn)
        {

            if (IsInitialized)
            {
                gameObjectToReturn.gameObject.SetActive(false);
                poolDictionary[type].Enqueue(gameObjectToReturn.gameObject);
            }
        }
        private bool IsPoolEmpty(PoolableType type)
        {
            if (!poolDictionary.ContainsKey(type))
            {
                Debug.LogWarning("Pool with tag " + type + " doesn't exist.");
                return false;
            }
            return poolDictionary[type].Count > 0;
        }

    }
