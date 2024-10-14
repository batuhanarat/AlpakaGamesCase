using UnityEngine;
[CreateAssetMenu(menuName = "AlpakaGamesCase/Decisions/LookDecision")]

public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }
    private bool Look(StateController controller)
    {
        Debug.Log("look result" + controller.OnAlarm);
            if(controller.OnAlarm) {
                controller.chaseTarget = Utilities.PlayerTransform;
                return true;
            }
            RaycastHit hit;
            Debug.DrawRay(controller.eyes.position,controller.eyes.forward.normalized * controller.enemyStats.lookRange, Color.cyan);

            if(Physics.SphereCast(controller.eyes.position,
                controller.enemyStats.lookSphereCastRadius,
                controller.eyes.forward,
                out hit, controller.enemyStats.lookRange)
                && hit.collider.CompareTag("Player"))
            {
                controller.chaseTarget = hit.transform;
                return true;
            } else
            {
                return false;
            }
    }

}
