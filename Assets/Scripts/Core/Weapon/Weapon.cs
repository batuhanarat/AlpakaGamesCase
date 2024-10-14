using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    private float lastAttackTime;

    public void Shoot(IAnimator animator, LayerMask targetLayer, IHumanoidController controller)
    {
        if (Time.time - lastAttackTime >= weaponData.cooldownTime)
        {
            animator.AttackAnimation();
            weaponData.Shoot(animator.Renderer.transform,
                            targetLayer,
                            controller);
            lastAttackTime = Time.time;
        }
    }
}
