using UnityEngine;

public interface IAnimator
{
    public Transform Renderer { get; }
    public void AttackAnimation();

}