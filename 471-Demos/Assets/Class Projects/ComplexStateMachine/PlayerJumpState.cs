using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private int doubleJumps = 0;
    public override void EnterState(PlayerStateManager playerStateManager)
    {
        Debug.Log("I'm Jumping");
        
        if (playerStateManager.canDoubleJump)
            doubleJumps = 1;    
        
        if(playerStateManager.controller.isGrounded)
            playerStateManager.verticalVelocity.y = playerStateManager.jumpHeight;
        else if (doubleJumps > 0)
        {
            playerStateManager.verticalVelocity.y = playerStateManager.jumpHeight;
            doubleJumps--;
        }
            
        playerStateManager.jumping = false;
    }

    public override void UpdateState(PlayerStateManager playerStateManager)
    {
        //What are we doing during this state?
        playerStateManager.MovePlayer(playerStateManager.default_speed);

        if (playerStateManager.jumping && doubleJumps > 0)
        {
            playerStateManager.verticalVelocity.y = playerStateManager.jumpHeight;
            doubleJumps--;
        }
        playerStateManager.jumping = false;
        
        //On what conditions do we leave the state?
        if (playerStateManager.controller.isGrounded)
        {
            Debug.Log("Grounded");
            if(playerStateManager.isSneaking)
                playerStateManager.SwitchState(playerStateManager.sneakState);
            else if (playerStateManager.movement.magnitude <= 0)
                playerStateManager.SwitchState(playerStateManager.idleState);
            else if (playerStateManager.movement.magnitude > 0)
                playerStateManager.SwitchState(playerStateManager.walkState);
        }
            
            
        
    }
}
