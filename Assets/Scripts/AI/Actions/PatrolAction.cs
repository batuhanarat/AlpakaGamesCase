using UnityEngine;

[CreateAssetMenu(menuName = "AlpakaGamesCase/Actions/PatrolAction")]
public class PatrolAction : EnemyAction
{
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        controller.navMeshAgent.destination = controller.wayPointList[controller.nextWayPoint].position;
        controller.navMeshAgent.isStopped = false;


        if(controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending){
            controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
        }
    }
}