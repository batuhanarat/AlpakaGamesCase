using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public interface IEnemyFactory {
    EnemyController Create(EnemyConfig config);
}
public class EnemyFactory : IEnemyFactory
{
    [SerializeField] bool collectionCheck = true;
    [SerializeField] int defaultCapacity = 10;
    [SerializeField] int maxPoolSize = 100;
    IObjectPool<EnemyController> enemyPool;
    readonly List<EnemyController> activeEnemies = new();
    void InitializePool(EnemyConfig config) {
            enemyPool = new ObjectPool<EnemyController>(
                () => {
                    var enemy = Object.Instantiate(config.prefab);
                    enemy.gameObject.SetActive(false);
                    EnemyController enemyComponent = enemy.GetComponent<EnemyController>();
                    //enemyComponent.Initialize(config);
                    return enemyComponent;
                },
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                collectionCheck,
                defaultCapacity,
                maxPoolSize);
        }

        EnemyController CreateEnemy(EnemyConfig config) {
            var enemy = Object.Instantiate(config.prefab);
            enemy.gameObject.SetActive(false);
            return enemy.GetComponent<EnemyController>();
        }

        void OnTakeFromPool(EnemyController enemy) {
            enemy.gameObject.SetActive(true);
            activeEnemies.Add(enemy);
        }

        void OnReturnedToPool(EnemyController enemy) {
            enemy.gameObject.SetActive(false);
            activeEnemies.Remove(enemy);
        }

        void OnDestroyPoolObject(EnemyController enemy) {
            Object.Destroy(enemy.gameObject);
        }
    public EnemyController Create(EnemyConfig config)
    {
        var enemy = Object.Instantiate(config.prefab);
        EnemyController enemyComponent = enemy.GetComponent<EnemyController>();
     //   enemyComponent.Initialize(config);
        return enemyComponent;
    }
}