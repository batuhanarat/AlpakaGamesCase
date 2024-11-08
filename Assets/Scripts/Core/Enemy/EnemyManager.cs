using System.Collections.Generic;
using DependencyInjection;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour, IDependencyProvider
{
    public Transform player;
    readonly Dictionary<EnemyType,IObjectPool<EnemyController>> enemyPools = new();
    public EnemyConfig config;
    [SerializeField] bool collectionCheck = true;
    [SerializeField] int defaultCapacity = 10;
    [SerializeField] int maxPoolSize = 100;
    private IObjectPool<EnemyController> enemyPool;
    private readonly List<EnemyController> activeEnemies = new();

    [Provide]
    public EnemyManager ProvideEnemyManager(){
        return this;
    }

    void Start() {
        InitializePools();
        PrePopulatePool(enemyPools[config.type],defaultCapacity);
    }
    void FixedUpdate()
    {
        foreach(var enemy in activeEnemies) {
            enemy.navMeshAgent.destination = player.position;
        }

    }
    public EnemyController GetEnemyWithType(EnemyType enemyType) {
        var pool = enemyPools[enemyType];
        return pool.Get();
    }
    public void ReturnToPool(EnemyController enemy) {
        var enemyType = config.type;
        var pool = enemyPools[config.type];
        pool.Release(enemy);
    }

    private void InitializePools() {
        enemyPools[config.type] = InitializePool(config);
    }
    private void PrePopulatePool(IObjectPool<EnemyController> pool, int defaultCapacity) {
        for(int i = 0 ; i<defaultCapacity ; i++) {
            var enemy = pool.Get();
            pool.Release(enemy);
        }
    }
    IObjectPool<EnemyController> InitializePool(EnemyConfig config) {
            enemyPool = new ObjectPool<EnemyController>(
                CreateNewEnemy,
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                collectionCheck,
                defaultCapacity,
                maxPoolSize);
            return enemyPool;
    }
    private EnemyController CreateNewEnemy()
    {
        var enemy = Instantiate(config.prefab);
        enemy.gameObject.SetActive(false);
        EnemyController enemyComponent = enemy.GetComponent<EnemyController>();
        //enemyComponent.Initialize(config);


        return enemyComponent;
    }

    private void OnTakeFromPool(EnemyController enemy) {
        enemy.gameObject.SetActive(true);
        activeEnemies.Add(enemy);
    }

    private void OnReturnedToPool(EnemyController enemy) {
        if(activeEnemies.Contains(enemy)) {
            activeEnemies.Remove(enemy);
            enemy.gameObject.SetActive(false);
        }

    }

    private void OnDestroyPoolObject(EnemyController enemy) {
        Destroy(enemy.gameObject);
    }

}
public enum EnemyType {
    HEADER,
    RANGED

}
