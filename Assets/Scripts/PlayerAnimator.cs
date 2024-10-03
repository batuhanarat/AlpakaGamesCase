using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerRenderer;
    [SerializeField] private float moveSpeedModifier;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ManageAnimation(Vector3 moveVector) {
        if(moveVector.magnitude>0) {
            animator.SetFloat("MoveSpeed", moveVector.magnitude * moveSpeedModifier);
            PlayRunAnimation();
            playerRenderer.forward = moveVector.normalized;
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
}
