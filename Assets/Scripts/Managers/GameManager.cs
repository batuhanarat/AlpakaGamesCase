using System.Collections;
using DependencyInjection;
using UnityEngine;

public  class GameManager: MonoBehaviour
{
    public static GameState CurrentState = GameState.PATROL;
    private float counter;
    [SerializeField] private float spawnInterval = 100f;
    public bool CanPlay { get; private set; }
    [Inject] private EnemySpawner EnemySpawner;

    private EventBinding<PlayerDiedEvent> playerDiedBinding;

    void Start()
    {
        counter = 0;
        StartGame();
    }
    void OnEnable() {
        playerDiedBinding = new EventBinding<PlayerDiedEvent>(StopGame);
        EventBus<PlayerDiedEvent>.Register(playerDiedBinding);
    }
    void OnDisable() {
        EventBus<PlayerDiedEvent>.Deregister(playerDiedBinding);
    }

    private void StopGame() {
        CanPlay = false;
    }

    private void StartGame() {
        CanPlay = true;
    }




    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {

            EnemySpawner.SpawnEnemies(10, EnemyType.HEADER);
        }
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

        }
    }



}