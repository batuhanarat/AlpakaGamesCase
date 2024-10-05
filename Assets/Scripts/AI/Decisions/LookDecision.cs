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
            RaycastHit hit;


            Debug.DrawRay(controller.eyes.position,controller.eyes.forward.normalized * controller.enemyStats.lookRange, Color.cyan);
            //LayerMask mask = LayerMask.GetMask("Player");

            if(Physics.SphereCast(controller.eyes.position,
                controller.enemyStats.lookSphereCastRadius,
                controller.eyes.forward,
                out hit, controller.enemyStats.lookRange))
            {
                // && hit.collider.CompareTag("Player")
                Debug.Log("collide with"+hit.collider.gameObject.name);
                controller.chaseTarget = hit.transform;
                return true;
            } else
            {
                return false;
            }
    }

}
