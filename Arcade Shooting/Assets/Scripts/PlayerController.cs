using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    PlayerData playerData;
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isJumpingHash;
    int isDeadHash;

    Vector2 currentMovementInput;
    Vector3 currtentMovement;
    Vector3 appliedMovement;

    bool isMovementPressed;
    float rotationFactorPerFrame = 15.0f;
    float runMultiplier = 8.0f;
    float groundedGravity = -0.5f;
    float gravity = -9.8f;

    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 3.0f;
    float maxJumpTime = 0.75f;
    bool isJumping = false;
    bool isJumpingAnimation = false;

    private void Awake()
    {
        playerData.CurrentScore = 0;
        playerData.CurrentDistance = 0;
        playerData.IsAlive = true;

        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("IsWalking");
        isJumpingHash = Animator.StringToHash("IsJumping");
        isDeadHash = Animator.StringToHash("IsDead");

        playerInput.CharacterControls.Move.started += context => {
            OnMovementInput(context);
        };
        playerInput.CharacterControls.Move.canceled += context => {
            OnMovementInput(context);
        };
        playerInput.CharacterControls.Move.performed += context => {
            OnMovementInput(context);
        };
        playerInput.CharacterControls.Jump.started += context => {
            OnJump(context);
        }; 
        playerInput.CharacterControls.Jump.canceled += context => {
            OnJump(context);
        };

        playerInput.CharacterControls.DeadTest.started += context => {
            OnDeadTest(context);
        };
        playerInput.CharacterControls.DeadTest.canceled += context => {
            OnDeadTest(context);
        };

        playerInput.CharacterControls.Pause.started += context => {
            OnPauseInput(context);
        };

        playerInput.CharacterControls.Pause.canceled += context => {
            OnPauseInput(context);
        };
        SetupJumpVariables();
    }


    void OnPauseInput(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            UIManagerController.Instance.PauseGame();
        }
    }
    void OnDeadTest(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            playerData.IsAlive = false;
        }
    }

    void HandleDead()
    {
        animator.SetBool(isDeadHash, true);
    }
    void SetupJumpVariables()
    {
        //este cálculo nace de que los saltos son una parábola simétrica, entonces
        //el tiempo que se tarda en llegar al zenit del salto, es la mitad del tiempo
        //que se tarda en realizar el salto completo
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void HandleJump() { 
        if(!isJumping && characterController.isGrounded && isJumpPressed)
        {
            animator.SetBool(isJumpingHash, true);
            isJumpingAnimation = true;
            isJumping = true;
            currtentMovement.y = initialJumpVelocity;
            appliedMovement.y = initialJumpVelocity;
        }else if(!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }
    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currtentMovement.x = currentMovementInput.y * runMultiplier;
        currtentMovement.z = -currentMovementInput.x * runMultiplier;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }
    void HandleGravity()
    {
        bool isFalling = currtentMovement.y <= 0.0f || !isJumpPressed;
        float fallingMultiplier = 2.0f;
        if (characterController.isGrounded)
        {
            if (isJumpingAnimation)
            {
                animator.SetBool(isJumpingHash, false);
                isJumpingAnimation = false;

            }
            appliedMovement.y = groundedGravity;
        }
        else if(isFalling)
        {
            float previousYVelocity = currtentMovement.y;
            currtentMovement.y = currtentMovement.y + (gravity * fallingMultiplier * Time.deltaTime);
            appliedMovement.y = (previousYVelocity + currtentMovement.y) * 0.5f;
        }else
        {
            float previousYVelocity = currtentMovement.y;
            currtentMovement.y = currtentMovement.y + (gravity * Time.deltaTime);
            appliedMovement.y = (previousYVelocity + currtentMovement.y) * 0.5f;
        }
    }
    void HandleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        if(isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }else if(!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);

        }
    }

    void HandleRotation()
    {
        Vector3 positionLookAt;
        positionLookAt.x = currtentMovement.x;
        positionLookAt.y = 0.0f;
        positionLookAt.z = currtentMovement.z;

        Quaternion currentRotation = transform.rotation;
        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void HandleTimeAlive()
    {
        playerData.CurrentDistance += Time.deltaTime;

    }
    private void FixedUpdate()
    {
        if (playerData.IsAlive)
        {
            HandleTimeAlive();
            HandleAnimation();
            HandleRotation();
            appliedMovement.x = currtentMovement.x;
            appliedMovement.z = currtentMovement.z;
            characterController.Move(appliedMovement * Time.deltaTime);

            HandleGravity();
            HandleJump();
        } else
        {
            HandleGravity();
            HandleDead();
        }
    }
    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }
    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Collectable"))
        {
            playerData.CurrentScore += other.gameObject.GetComponent<Collectable>().getValue();
        }
        else if (other.gameObject.CompareTag("DeadWall"))
        {
            playerData.IsAlive = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadWall"))
        {
            playerData.IsAlive = false;
        }
    }
}
