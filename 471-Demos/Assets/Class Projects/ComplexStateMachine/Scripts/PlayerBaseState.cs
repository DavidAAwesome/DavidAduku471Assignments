using UnityEngine;

// public abstract class PlayerBaseState
// {
//     public abstract void EnterState(PlayerStateManager playerStateManager);
//     
//     public abstract void UpdateState(PlayerStateManager playerStateManager);
//     
// }

public abstract class PlayerBaseState
{
    protected PlayerStateManager character;
    
    public PlayerBaseState(PlayerStateManager character)
    {
        this.character = character;
    }
    
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}