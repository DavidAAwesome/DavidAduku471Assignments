using UnityEngine;
//
// public class PlayerJumpState : PlayerBaseState
// {
//     private int doubleJumps = 0;
//     public override void EnterState(PlayerStateManager playerStateManager)
//     {
//         Debug.Log("I'm Jumping");
//         
//         if (playerStateManager.canDoubleJump)
//             doubleJumps = 1;    
//         
//         if(playerStateManager.controller.isGrounded)
//             playerStateManager.verticalVelocity.y = playerStateManager.jumpHeight;
//         else if (doubleJumps > 0)
//         {
//             playerStateManager.verticalVelocity.y = playerStateManager.jumpHeight;
//             doubleJumps--;
//         }
//             
//         playerStateManager.jumping = false;
//     }
//
//     public override void UpdateState(PlayerStateManager playerStateManager)
//     {
//         //What are we doing during this state?
//
//         if (playerStateManager.jumping && doubleJumps > 0)
//         {
//             playerStateManager.verticalVelocity.y = playerStateManager.jumpHeight;
//             doubleJumps--;
//             playerStateManager.jumping = false;
//         }
//         
//         playerStateManager.MovePlayer(playerStateManager.default_speed);
//         //On what conditions do we leave the state?
//         if (playerStateManager.controller.isGrounded)
//         {
//             Debug.Log("Grounded");
//             if(playerStateManager.isSneaking)
//                 playerStateManager.SwitchState(playerStateManager.sneakState);
//             else if (playerStateManager.movement.magnitude <= 0)
//                 playerStateManager.SwitchState(playerStateManager.idleState);
//             else if (playerStateManager.movement.magnitude > 0)
//                 playerStateManager.SwitchState(playerStateManager.walkState);
//         }
//             
//             
//         
//     }
// }

public class PlayerJumpState : PlayerBaseState
{
    private bool hasDoubleJumped;
    
    public PlayerJumpState(PlayerStateManager character) : base(character) { }
    
    public override void Enter()
    {
        // character.anim.SetInteger("State", 2);
        if(character.IsGrounded())
            hasDoubleJumped = false;
        else
            hasDoubleJumped = true;
        character.Jump();
        
    }
    
    public override void Update()
    {
        character.Move(character.airControlSpeed);
        if (character.playerInput.actions["Jump"].WasPressedThisFrame() && character.canDoubleJump && !hasDoubleJumped)
        {
            character.Jump();
            hasDoubleJumped = true;
        }
        if (character.IsGrounded()) character.ChangeState(new PlayerIdleState(character));
    }
}
