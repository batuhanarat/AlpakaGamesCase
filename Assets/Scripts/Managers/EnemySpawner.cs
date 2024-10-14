using System.Collections.Generic;
using Game.Managers;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IProvidable
{
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
    [SerializeField] private GameObject _enemyPrefab;

    private void Awake()
    {
        ServiceProvider.Register(this);
    }

    private Transform GetRandomSpawnPoint()
    {
        var size = _spawnPoints.Count;
        var randomIndex =  Random.Range(0,size);
        return _spawnPoints[randomIndex];
    }

    public Transform SpawnEnemyInRandomLocation()
    {
        Transform spawnPoint = GetRandomSpawnPoint();
        var enemy  = ServiceProvider.Pool.SpawnFromPool(PoolableType.Enemy);
        enemy.transform.SetPositionAndRotation(spawnPoint.position, Quaternion.identity);
        ExecuteSpawnAnimation(enemy);
        return enemy.transform;
    }

    public void SpawnEnemyWaveInRandomLocation(int enemyCountInWave)
    {
        Transform spawnPoint = GetRandomSpawnPoint();
        var enemyTransformOffset = new Vector3(2f,0,2f);
        for(int i = 0; i< enemyCountInWave ; i++) {
            var enemy  = ServiceProvider.Pool.SpawnFromPool(PoolableType.Enemy);
            enemy.transform.SetPositionAndRotation(spawnPoint.position+ enemyTransformOffset *i, Quaternion.identity);
            ExecuteSpawnAnimation(enemy);
        }
    }

    public void ExecuteSpawnAnimation(GameObject enemy) {
        var enemyAnimator = enemy.GetComponent<EnemyAnimator>();
        enemyAnimator.PlaySpawnAnimation();
    }
}