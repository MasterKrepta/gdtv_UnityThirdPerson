using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    readonly int IMPACT_HASH = Animator.StringToHash("Impact");
    const float CROSSfADE_TIME = 0.1f;
    float duration = 1;
    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.Anim.CrossFadeInFixedTime(IMPACT_HASH, CROSSfADE_TIME);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if (duration <= 0)
        {
            ReturnToLocomotion();
            
        }
    }
    public override void Exit()
    {
        
    }

}
