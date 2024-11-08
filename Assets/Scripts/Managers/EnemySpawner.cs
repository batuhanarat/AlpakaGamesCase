using AudioSystem;
using DependencyInjection;
using UnityEngine;


public class EnemySpawner : MonoBehaviour, IDependencyProvider
{
    [Provide]
    public EnemySpawner ProvideEnemySpawner() {
        return this;
    }


    [Inject] PlayerController player;
    [Inject] SoundManager soundManager;
    [Inject] EnemyManager enemyManager;

    public PlacementStrategy placementStrategy;

    [SerializeField] SoundData soundData;



    public void PlaySpawnSound() {
        SoundBuilder soundBuilder = soundManager.CreateSoundBuilder();

        soundBuilder
            .WithRandomPitch()
            .WithPosition(transform.position)
            .Play(soundData);
    }

    public void SpawnEnemies(int enemyCount, EnemyType type) {
        for(int i = 0 ; i< enemyCount ; i++) {
            EnemyController enemy = enemyManager.GetEnemyWithType(type);
            enemy.transform.position = placementStrategy.SetPosition(player.transform.position);
            PlaySpawnSound();
        }
    }


}