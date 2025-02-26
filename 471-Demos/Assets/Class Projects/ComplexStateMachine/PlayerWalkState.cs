using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager playerStateManager)
    {
        Debug.Log("I'm Walking");
    }

    public override void UpdateState(PlayerStateManager playerStateManager)
    {
        //What are we doing during this state?
        playerStateManager.MovePlayer(playerStateManager.default_speed);
        
        //On what conditions do we leave the state?
        if(playerStateManager.jumping)
            playerStateManager.SwitchState(playerStateManager.jumpState);
        else if (playerStateManager.movement.magnitude <= 0)
            playerStateManager.SwitchState(playerStateManager.idleState);
        else if(playerStateManager.isSneaking)
            playerStateManager.SwitchState(playerStateManager.sneakState);
    }
    
}
