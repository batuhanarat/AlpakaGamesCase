using UnityEngine;

public class WideRangeController : RangeController
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
        EventBus<EnemySpottedInWideRangeEvent>.Raise(new EnemySpottedInWideRangeEvent{
            enemiesInRange = enemiesInRange});
    }

}