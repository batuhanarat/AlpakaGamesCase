using UnityEngine;

[CreateAssetMenu(menuName = "AlpakaGamesCase/ ActiveStateDecision")]
public class ActiveStateDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool chaseTargetIsActive = controller.chaseTarget.gameObject.activeSelf;
        return chaseTargetIsActive;
    }
}