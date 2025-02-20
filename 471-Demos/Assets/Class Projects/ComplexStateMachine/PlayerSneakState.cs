using UnityEngine;

public class PlayerSneakState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager playerStateManager)
    {
        Debug.Log("I'm Sneaking");
    }

    public override void UpdateState(PlayerStateManager playerStateManager)
    {
        //What are we doing during this state?
        playerStateManager.MovePlayer(playerStateManager.default_speed/2.0f);
        
        //On what conditions do we leave the state?
        if (playerStateManager.movement.magnitude <= 0)
            playerStateManager.SwitchState(playerStateManager.idleState);
        else if (!playerStateManager.isSneaking)
            playerStateManager.SwitchState(playerStateManager.walkState);
    }
}
