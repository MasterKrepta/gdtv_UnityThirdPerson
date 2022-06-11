using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    readonly int LOCOMOTION_HASH = Animator.StringToHash("Locomotion");
    readonly int SPEED_HASH = Animator.StringToHash("Speed");

    private const float CROSSFADE_DUR = 0.1F;
    private const float ANIM_DAMP_TIME = 0.1F;

    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.Anim.CrossFadeInFixedTime(LOCOMOTION_HASH, CROSSFADE_DUR);
        
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if (IsInChaseRange())
        {
            _stateMachine.SwitchState(new EnemyChasingState(_stateMachine));
            return;
        }
        _stateMachine.Anim.SetFloat(SPEED_HASH, 0f, ANIM_DAMP_TIME, deltaTime);
        
    }

    public override void Exit()  { }



}

