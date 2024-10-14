using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy Weapon", menuName = "Weapons/Enemy Touch Weapon")]
public class EnemyTouchWeaponData : WeaponData
{

    public override void Shoot(Transform attacker, LayerMask targetLayer, IHumanoidController controller)
    {

        RaycastHit hit;
        if (Physics.Raycast(attacker.position, attacker.forward, out hit, attackRange, targetLayer))
        {
            PlayerController player = hit.collider.GetComponent<PlayerController>();
            if (player != null)
            {
                player.GetHit();
                Debug.Log("Player hit!");
            }

        }


    }
}