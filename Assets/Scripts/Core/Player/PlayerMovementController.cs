using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerAnimator animator;
    private PlayerState currentState;
    private EventBinding<PlayerStateUpdatedEvent> playerStateUpdatedEventBinding;
    public Transform TargetLocation {get; set;}
    [SerializeField] private float moveSpeed;
    [SerializeField] private MobileJoystick mobileJoystick;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<PlayerAnimator>();
    }
    void Start()
    {
        currentState = PlayerState.PATROL;

    }
    void Update()
    {
        ControlMovement();
    }
    void OnEnable()
    {
        playerStateUpdatedEventBinding = new EventBinding<PlayerStateUpdatedEvent>(OnPlayerStateUpdated);
        EventBus<PlayerStateUpdatedEvent>.Register(playerStateUpdatedEventBinding);
    }
    void OnDisable()
    {
        EventBus<PlayerStateUpdatedEvent>.Deregister(playerStateUpdatedEventBinding);
    }
    private void ControlMovement()
    {
        gameObject.transform.position = new Vector3(transform.position.x,0,transform.position.z);
        Vector3 correctedMoveVector =  mobileJoystick.GetMoveVector();
        correctedMoveVector.z = correctedMoveVector.y;
        correctedMoveVector.y = 0;
        animator.ManageMovementAnimation(correctedMoveVector,TargetLocation,currentState);


        Vector3 moveVector = mobileJoystick.GetMoveVector() * moveSpeed * Time.deltaTime;
        moveVector.z = moveVector.y;
        moveVector.y = 0;
        characterController.Move(moveVector);
    }
    private void OnPlayerStateUpdated(PlayerStateUpdatedEvent playerStateUpdatedEvent) {
        currentState = playerStateUpdatedEvent.newState;
    }

}