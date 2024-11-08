using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private RangedWeapon rangedWeapon;
    [SerializeField] private AreaDamageWeapon areaDamageWeapon;


    private EventBinding<EnemySpottedInWideRangeEvent> enemySpottedInWideRangeEventBinding;
    private EventBinding<EnemySpottedInShortRangeEvent> enemySpottedInShortRangeEventBinding;

    private WideRangeController wideRangeDetector;
    private CloseRangeController closeRangeDetector;
    private List<EnemyController> enemiesInWideRange = new();
    private List<EnemyController> enemiesInShortRange = new();

    private int _totalCountOfEnemiesInRange = 0;
    public bool IsEnemyExistInRange => _totalCountOfEnemiesInRange > 0 ;

    void Awake()
    {
        wideRangeDetector = GetComponent<WideRangeController>();
        closeRangeDetector = GetComponent<CloseRangeController>();
    }
    void OnEnable()
    {
        enemySpottedInWideRangeEventBinding = new EventBinding<EnemySpottedInWideRangeEvent>(OnWideRangeUpdated);
        EventBus<EnemySpottedInWideRangeEvent>.Register(enemySpottedInWideRangeEventBinding);
        enemySpottedInShortRangeEventBinding = new EventBinding<EnemySpottedInShortRangeEvent>(OnShortRangeUpdated);
        EventBus<EnemySpottedInShortRangeEvent>.Register(enemySpottedInShortRangeEventBinding);
    }
    void Update()
    {
        if(!IsEnemyExistInRange) return;
        TryAttack();
    }
    void OnDisable()
    {
        EventBus<EnemySpottedInWideRangeEvent>.Deregister(enemySpottedInWideRangeEventBinding);
        EventBus<EnemySpottedInShortRangeEvent>.Deregister(enemySpottedInShortRangeEventBinding);
    }
    private void TryAttack()
    {
        if(rangedWeapon.CanAttack)
        {
            var closestEnemy = FindClosestTarget(enemiesInWideRange);
            transform.LookAt(closestEnemy.transform);
            rangedWeapon.Attack(closestEnemy);
        }
        if(areaDamageWeapon.CanAttack)
        {
            areaDamageWeapon.Attack(enemiesInShortRange);
        }

    }
    private void OnWideRangeUpdated(EnemySpottedInWideRangeEvent rangeUpdatedEvent)
    {
        _totalCountOfEnemiesInRange = rangeUpdatedEvent.enemiesInRange.Count;
        enemiesInWideRange = rangeUpdatedEvent.enemiesInRange;
    }
    private void OnShortRangeUpdated(EnemySpottedInShortRangeEvent rangeUpdatedEvent)
    {
        enemiesInShortRange = rangeUpdatedEvent.enemiesInRange;
    }
    public EnemyController FindClosestTarget(List<EnemyController> enemiesInRange)
    {
        if (enemiesInRange == null || enemiesInRange.Count == 0)
            return null;

        return enemiesInRange
            .OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position))
            .FirstOrDefault();
    }
}
