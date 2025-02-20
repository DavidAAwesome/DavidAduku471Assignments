using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    private CharacterController controller;

    [HideInInspector] public PlayerBaseState currentState;
    [HideInInspector] public PlayerIdleState idleState = new PlayerIdleState();
    [HideInInspector] public PlayerWalkState walkState = new PlayerWalkState();
    [HideInInspector] public PlayerSneakState sneakState = new PlayerSneakState();
    
    public Vector2 movement;
    public float default_speed = 5f;
    public  bool isSneaking = false;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        SwitchState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
    
    // Handle Input //

    public void OnMove(InputValue moveVal)
    {
        movement = moveVal.Get<Vector2>();
    }

    public void OnSprint(InputValue sprintVal)
    {
        if (sprintVal.isPressed)
            isSneaking = true;
        else
            isSneaking = false;
    }
    
    // Helper Functions //

    public void MovePlayer(float speed)
    {
        float moveX = movement.x;
        float moveZ = movement.y;
        
        Vector3 actual_movement = new Vector3(moveX, 0, moveZ);
        controller.Move(actual_movement * (speed * Time.deltaTime));
    }

    public void SwitchState(PlayerBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
}
