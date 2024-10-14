using UnityEngine;

[CreateAssetMenu(menuName = "AlpakaGamesCase/Actions/ChaseAction")]
public class ChaseAction : EnemyAction
{
    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    private void Chase(StateController controller)
    {
        controller.navMeshAgent.destination = controller.chaseTarget.position;
        controller.navMeshAgent.isStopped = false;
    }
}
