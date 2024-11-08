using UnityEngine;

public class CloseRangeController : RangeController
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var enemy = other.gameObject.GetComponent<EnemyController>();
            enemiesInRange.Add(enemy);
            RaiseRangeUpdated();
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var enemy = other.gameObject.GetComponent<EnemyController>();
            enemiesInRange.Remove(enemy);
            RaiseRangeUpdated();
        }
    }
    private void RaiseRangeUpdated()
    {
        EventBus<EnemySpottedInShortRangeEvent>.Raise(new EnemySpottedInShortRangeEvent{
            enemiesInRange = enemiesInRange});
    }
}
