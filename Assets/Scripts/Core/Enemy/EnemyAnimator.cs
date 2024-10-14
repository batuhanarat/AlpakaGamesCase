using UnityEngine;

public class EnemyAnimator : MonoBehaviour,IAnimator
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform _renderer;

    public Transform Renderer => _renderer;
    public void AttackAnimation()
    {
        HeadAttackAnimation();
    }
    public void DeadAnimation()
    {
        animator.Play("Die");
    }
    public void TakeDamageAnimation()
    {
        animator.Play("Take Damage");
    }
    public void SpinAttackAnimation()
    {
        animator.Play("Spin Attack");
    }
    public void HeadAttackAnimation()
    {
        animator.Play("Head Attack");

    }
    public void PlaySpawnAnimation()
    {
        animator.Play("Spawn");
    }
    public void FlyForwardAnimation()
    {
        animator.Play("Fly Forward In Place");
    }
}