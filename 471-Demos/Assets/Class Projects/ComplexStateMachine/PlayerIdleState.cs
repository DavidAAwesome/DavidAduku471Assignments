using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager playerStateManager)
    {
        Debug.Log("I'm Idling");
    }

    public override void UpdateState(PlayerStateManager playerStateManager)
    {
        //What are we doing during this state?
        //Nothing!
        
        //On what conditions do we leave the state?
        if (playerStateManager.jumping)
        {
            playerStateManager.jumping = false;
            playerStateManager.SwitchState(playerStateManager.jumpState);
        }
        else if (playerStateManager.movement.magnitude > 0)
        {
            if (playerStateManager.isSneaking)
            {
                playerStateManager.SwitchState(playerStateManager.sneakState);
            }
            else
            {
                playerStateManager.SwitchState(playerStateManager.walkState);
            }
            
        }
        else if(playerStateManager.isSneaking)
            playerStateManager.SwitchState(playerStateManager.sneakState);
        
    }

    
}
