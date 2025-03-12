using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// public class PlayerStateManager : MonoBehaviour
// {
//     [HideInInspector] public CharacterController controller;
//     [HideInInspector] public PlayerBaseState currentState;
//     [HideInInspector] public PlayerIdleState idleState = new PlayerIdleState();
//     [HideInInspector] public PlayerWalkState walkState = new PlayerWalkState();
//     [HideInInspector] public PlayerSneakState sneakState = new PlayerSneakState();
//     [HideInInspector] public PlayerJumpState jumpState = new PlayerJumpState();
//     public GameObject winText;
//     
//     public Vector2 movement;
//     public float default_speed = 5f;
//     public  bool isSneaking = false;
//     public bool isHiding = false;
//     public bool jumping = false;
//     public bool canDoubleJump = false;
//     public bool lavaImmune = false;
//     
//     // Gravity variables
//     public float gravity = -3f;
//     public float jumpHeight = 2f;
//     public Vector3 verticalVelocity;
//    
//     
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         verticalVelocity.y = 0;
//         controller = GetComponent<CharacterController>();
//         SwitchState(idleState);
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//         currentState.UpdateState(this);
//         
//         // Handle gravity
//         if (!controller.isGrounded)
//             verticalVelocity.y += gravity * Time.deltaTime;
//         controller.Move(verticalVelocity);
//         
//         if (transform.position.y < -15)
//         {
//             KillPlayer();
//         }
//     }
//     
//     // Handle Input //
//
//     public void OnMove(InputValue moveVal)
//     {
//         movement = moveVal.Get<Vector2>();
//     }
//
//     public void OnSprint(InputValue sprintVal)
//     {
//         if (sprintVal.isPressed)
//             isSneaking = true;
//         else
//             isSneaking = false;
//     }
//
//     public void OnJump()
//     {
//         jumping = true;
//     }
//     
//     // Helper Functions //
//
//     public void MovePlayer(float speed)
//     {
//         float moveX = movement.x;
//         float moveZ = movement.y;
//         
//         Vector3 actual_movement = new Vector3(moveX, 0, moveZ);
//         if ((actual_movement * (speed * Time.deltaTime)).Equals(Vector3.zero))
//         {
//             Debug.Log("Was Zero");
//             controller.Move(new Vector3(0.001f, 0, 0));
//         }
//         else
//             controller.Move(actual_movement * (speed * Time.deltaTime));
//     }
//
//     public void SwitchState(PlayerBaseState newState)
//     {
//         currentState = newState;
//         currentState.EnterState(this);
//     }
//
//     public void KillPlayer()
//     {
//         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//     }
//     
//     
//     public void OnTriggerEnter(Collider other)
//     {
//         if (other.gameObject.CompareTag("Enemy"))
//             KillPlayer();
//         if (other.gameObject.CompareTag("Player")) // Lava
//         {
//             if(!lavaImmune)
//                 KillPlayer();
//         }
//         if(other.gameObject.CompareTag("Bush"))
//             if (currentState is PlayerSneakState)
//             {
//                 print("I'm hiding");
//                 isHiding = true;
//             }
//         if (other.gameObject.CompareTag("Killer"))//JumpTrigger
//         {
//             print("Hit JumpTrigger");
//             Destroy(other.gameObject);
//             canDoubleJump = true;
//         }
//         if (other.gameObject.CompareTag("Ground"))//LavaImmuneTrigger
//         {
//             print("Hit LavaImmuneTrigger");
//             Destroy(other.gameObject);
//             lavaImmune = true;
//         }
//         if (other.gameObject.CompareTag("Finish"))
//         {
//             print("Hit Finish");
//             Destroy(other.gameObject);
//             winText.SetActive(true);
//         }
//                 
//     }
//
//     public void OnTriggerStay(Collider other)
//     {
//         if(other.gameObject.CompareTag("Bush"))
//             if (currentState is PlayerSneakState)
//             {
//                 print("I'm hiding");
//                 isHiding = true;
//             }
//     }
//
//     public void OnTriggerExit(Collider other)
//     {
//         if (other.gameObject.CompareTag("Bush"))
//         {
//             Debug.Log("Hiding is false");
//             isHiding = false;
//         }
//             
//     }
// }



    

 
        
        
public class PlayerStateManager : MonoBehaviour
{
    public PlayerBaseState currentState;
    public CharacterController characterController;
    public PlayerInput playerInput;
    public float walkSpeed = 5f;
    public float sneakSpeed = 2.5f;
    public float jumpForce = 5f;
    public float airControlSpeed = 3f;
    private Vector3 velocity;
    public float gravity = -9.81f;
    public bool canDoubleJump = true;
    
    public bool isHiding = false;
    public bool lavaImmune = false;
    public GameObject winText;

    // public Animator anim;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        // anim = GetComponent<Animator>();
        ChangeState(new PlayerIdleState(this));
    }
    
    void Update()
    {
        currentState.Update();
        ApplyGravity();
        if (transform.position.y < -15) 
            KillPlayer();
    }
    
    public void ChangeState(PlayerBaseState newState)
    {
        if (currentState != null) currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
    
    public void Move(float speed)
    {
        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        if (move.magnitude > 0)
        {
            transform.forward = move.normalized;
        }
        characterController.Move(move.normalized * speed * Time.deltaTime);
    }
    
    public void Jump()
    {
        velocity.y = jumpForce;
    }
    
    public bool IsGrounded()
    {
        return characterController.isGrounded;
    }
    
    private void ApplyGravity()
    {
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f;
        }
        characterController.Move(velocity * Time.deltaTime);
    }
    
    public void KillPlayer()
     {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     }
     
     
     public void OnTriggerEnter(Collider other)
     {
         if (other.gameObject.CompareTag("Enemy"))
             KillPlayer();
         if (other.gameObject.CompareTag("Player")) // Lava
         {
             if(!lavaImmune)
                 KillPlayer();
         }
         if(other.gameObject.CompareTag("Bush"))
             if (currentState is PlayerSneakState)
             {
                 print("I'm hiding");
                 isHiding = true;
             }
         if (other.gameObject.CompareTag("Killer"))//JumpTrigger
         {
             print("Hit JumpTrigger");
             Destroy(other.gameObject);
             canDoubleJump = true;
         }
         if (other.gameObject.CompareTag("Ground"))//LavaImmuneTrigger
         {
             print("Hit LavaImmuneTrigger");
             Destroy(other.gameObject);
             lavaImmune = true;
         }
         if (other.gameObject.CompareTag("Finish"))
         {
             print("Hit Finish");
             Destroy(other.gameObject);
             winText.SetActive(true);
             SceneManager.LoadScene("WinScene");
         }
                 
     }

     public void OnTriggerStay(Collider other)
     {
         if(other.gameObject.CompareTag("Bush"))
             if (currentState is PlayerSneakState)
             {
                 print("I'm hiding");
                 isHiding = true;
             }
     }

     public void OnTriggerExit(Collider other)
     {
         if (other.gameObject.CompareTag("Bush"))
         {
             Debug.Log("Hiding is false");
             isHiding = false;
         }
             
     }
}

