using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private MobileJoystick mobileJoystick;
    [SerializeField] private PlayerAnimator animator;
    private CharacterController characterController;
    [SerializeField] private float moveSpeed;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        ControlMovement();
    }

    private void ControlMovement()
    {
        Vector3 correctedMoveVector =  mobileJoystick.GetMoveVector();
        correctedMoveVector.z = correctedMoveVector.y;
        correctedMoveVector.y = 0;
        animator.ManageAnimation(correctedMoveVector);

        Vector3 moveVector = mobileJoystick.GetMoveVector() * moveSpeed * Time.deltaTime;
        moveVector.z = moveVector.y;
        moveVector.y = 0;
        characterController.Move(moveVector);
    }
}
