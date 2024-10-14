using UnityEngine;

[CreateAssetMenu(menuName = "AlpakaGamesCase/Actions/Attack")]
public class AttackAction : EnemyAction
{

    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {

        controller.ManageAttack();
    }
}