using UnityEngine;
//
// public class PlayerIdleState : PlayerBaseState
// {
//     public override void EnterState(PlayerStateManager playerStateManager)
//     {
//         Debug.Log("I'm Idling");
//     }
//
//     public override void UpdateState(PlayerStateManager playerStateManager)
//     {
//         //What are we doing during this state?
//         //Nothing!
//         
//         //On what conditions do we leave the state?
//         if (playerStateManager.jumping)
//         {
//             playerStateManager.SwitchState(playerStateManager.jumpState);
//         }
//         else if (playerStateManager.movement.magnitude > 0)
//         {
//             if (playerStateManager.isSneaking)
//             {
//                 playerStateManager.SwitchState(playerStateManager.sneakState);
//             }
//             else
//             {
//                 playerStateManager.SwitchState(playerStateManager.walkState);
//             }
//             
//         }
//         else if(playerStateManager.isSneaking)
//             playerStateManager.SwitchState(playerStateManager.sneakState);
//         
//     }
//
//     
// }

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateManager character) : base(character) { }

    public override void Enter()
    {
        // character.anim.SetInteger("State", 0);
    }
    
    public override void Update()
    {
        if (character.playerInput.actions["Sprint"].IsPressed()) character.ChangeState(new PlayerSneakState(character));
        else if (character.playerInput.actions["Move"].ReadValue<Vector2>().magnitude > 0) character.ChangeState(new PlayerWalkState(character));
        else if (character.playerInput.actions["Jump"].WasPressedThisFrame()) character.ChangeState(new PlayerJumpState(character));
    }
}
