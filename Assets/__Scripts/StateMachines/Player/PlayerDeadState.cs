using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //toggle ragdoll physics
        _stateMachine.WeaponDamage.gameObject.SetActive(false);
    }
    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {
        
    }

    


}
