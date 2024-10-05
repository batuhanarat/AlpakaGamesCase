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

    private void ControlAttack() {

    }

    private void Attack(IWeapon weapon) {

        // get weapon range - ortak



        //find all contacts with weapon range
        //find all contacts with weapon range

            //for zone -
                //instantiate a increasing circle on the scene according to range
                //weapon hit damage to all enemies in the range

            //for pistol -
                //-get the nearest enemy on the range
                //instantiate a bullet from pistol
                //direct bullet that follow to the enemy position




    }
}
