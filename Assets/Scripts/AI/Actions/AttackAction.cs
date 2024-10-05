using UnityEngine;

[CreateAssetMenu(menuName = "AlpakaGamesCase/Actions/Attack")]
public class AttackAction : Action
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller) {

            RaycastHit hit;


            Debug.DrawRay(controller.eyes.position,controller.eyes.forward.normalized * controller.enemyStats.attackRange, Color.red);
            //LayerMask mask = LayerMask.GetMask("Player");

            if(Physics.SphereCast(controller.eyes.position,
                controller.enemyStats.lookSphereCastRadius,
                controller.eyes.forward,
                out hit, controller.enemyStats.attackRange))
            {
                if(controller.CheckIfCountDownElapsed(controller.enemyStats.attackRate)) {
                   // controller.weapon.Fire(controller.enemyStats.attackForce, controller.enemyStats.attackRate);
                }
            }
    }
}