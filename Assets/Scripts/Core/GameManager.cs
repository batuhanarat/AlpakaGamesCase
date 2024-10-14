using System.Collections;
using Game.Managers;
using UnityEngine;

public  class GameManager: MonoBehaviour, IProvidable
{
    public static GameState CurrentState = GameState.PATROL;
    private float counter;

    [SerializeField] private float spawnInterval = 1000f;
    public bool CanPlay { get; set; }


    void Awake()
    {
        ServiceProvider.Register(this);
        counter = 0;
        CanPlay = true;
    }


    private void Start()
    {
        if(CanPlay) {
            StartCoroutine(SpawnEnemyRoutine());
        }
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            ServiceProvider.EnemySpawner.SpawnEnemyInRandomLocation();
        }
    }



}