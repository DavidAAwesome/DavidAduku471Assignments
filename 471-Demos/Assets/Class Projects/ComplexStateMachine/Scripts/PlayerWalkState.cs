using UnityEngine;
//
// public class PlayerWalkState : PlayerBaseState
// {
//     public override void EnterState(PlayerStateManager playerStateManager)
//     {
//         Debug.Log("I'm Walking");
//     }
//
//     public override void UpdateState(PlayerStateManager playerStateManager)
//     {
//         //What are we doing during this state?
//         playerStateManager.MovePlayer(playerStateManager.default_speed);
//         
//         //On what conditions do we leave the state?
//         if(playerStateManager.jumping)
//             playerStateManager.SwitchState(playerStateManager.jumpState);
//         else if (playerStateManager.movement.magnitude <= 0)
//             playerStateManager.SwitchState(playerStateManager.idleState);
//         else if(playerStateManager.isSneaking)
//             playerStateManager.SwitchState(playerStateManager.sneakState);
//     }
//     
// }

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateManager character) : base(character) { }
    
    public override void Enter()
    {
        // character.anim.SetInteger("State", 1);
    }
    
    public override void Update()
    {
        character.Move(character.walkSpeed);
        if (character.playerInput.actions["Sprint"].IsPressed()) character.ChangeState(new PlayerSneakState(character));
        else if (character.playerInput.actions["Jump"].WasPressedThisFrame()) character.ChangeState(new PlayerJumpState(character));
        else if (character.playerInput.actions["Move"].ReadValue<Vector2>().magnitude == 0) character.ChangeState(new PlayerIdleState(character));
    }
}
