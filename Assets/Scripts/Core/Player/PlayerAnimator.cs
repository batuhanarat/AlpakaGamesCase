using UnityEngine;

public class PlayerAnimator : MonoBehaviour,IAnimator
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform _playerRenderer;

    public Transform Renderer  => _playerRenderer ;

    [SerializeField] private float moveSpeedModifier;



    public void ManageMovementAnimation(Vector3 moveVector,Transform targetTransform,PlayerState playerState) {
        if(moveVector.magnitude>0) {
            animator.SetFloat("MoveSpeed", moveVector.magnitude * moveSpeedModifier);
            PlayRunAnimation();
            if(playerState == PlayerState.PATROL)
            {
                //lookDirection = moveVector.normalized;
                Renderer.forward = moveVector.normalized;
            }

            if(playerState == PlayerState.COMBAT) {
                if(targetTransform == null  ) return;
                Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;
                directionToTarget.y = 0;
                Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
                Renderer.rotation = Quaternion.Slerp(Renderer.rotation, lookRotation, 5f * Time.deltaTime);
                }
        } else {
            PlayIdleAnimation();
        }
    }

    private void PlayIdleAnimation()
    {
        animator.Play("Idle");
    }

    private void PlayRunAnimation()
    {
        animator.Play("Run");
    }

    public void AttackAnimation() {
        animator.Play("Shoot", 1, 0f);
    }

}


