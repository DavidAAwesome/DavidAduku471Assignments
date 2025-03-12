using UnityEngine;
//
// public class PlayerSneakState : PlayerBaseState
// {
//     public override void EnterState(PlayerStateManager playerStateManager)
//     {
//         Debug.Log("I'm Sneaking");
//     }
//
//     public override void UpdateState(PlayerStateManager playerStateManager)
//     {
//         //What are we doing during this state?
//         playerStateManager.MovePlayer(playerStateManager.default_speed/2.0f);
//         
//         //On what conditions do we leave the state?
//         if(playerStateManager.jumping)
//             playerStateManager.SwitchState(playerStateManager.jumpState);
//         else if (playerStateManager.movement.magnitude <= 0 && !playerStateManager.isSneaking)
//         {
//             Debug.Log("Hiding is false");
//             playerStateManager.isHiding = false;
//             playerStateManager.SwitchState(playerStateManager.idleState);
//         }
//         else if (!playerStateManager.isSneaking)
//         {
//             Debug.Log("Hiding is false");
//             playerStateManager.isHiding = false;
//             playerStateManager.SwitchState(playerStateManager.walkState);
//         }
//             
//     }
//     
//     
// }

public class PlayerSneakState : PlayerBaseState
{
    public PlayerSneakState(PlayerStateManager character) : base(character) { }
    
    public override void Enter()
    {
        // character.anim.SetInteger("State", 1);
    }
    
    public override void Update()
    {
        character.Move(character.sneakSpeed);
        if (!character.playerInput.actions["Sprint"].IsPressed()) character.ChangeState(new PlayerWalkState(character));
        else if (character.playerInput.actions["Jump"].WasPressedThisFrame()) character.ChangeState(new PlayerJumpState(character));
    }
}
