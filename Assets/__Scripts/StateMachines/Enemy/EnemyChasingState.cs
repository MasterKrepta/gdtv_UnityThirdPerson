using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    readonly int LOCOMOTION_HASH = Animator.StringToHash("Locomotion");
    readonly int SPEED_HASH = Animator.StringToHash("Speed");

    private const float CROSSFADE_DUR = 0.1F;
    private const float ANIM_DAMP_TIME = 0.1F;

    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }


    public override void Enter()
    {
        _stateMachine.Anim.CrossFadeInFixedTime(LOCOMOTION_HASH, CROSSFADE_DUR);

    }

    public override void Tick(float deltaTime)
    {
        

        if (!IsInChaseRange())
        {
            _stateMachine.SwitchState(new EnemyIdleState(_stateMachine));
            return;
        }
        else if (IsInAttackRange())
        {
            _stateMachine.SwitchState(new EnemyAttackingState(_stateMachine));
            return;
        }


        MoveToPlayer(deltaTime);
        FacePlayer();

        _stateMachine.Anim.SetFloat(SPEED_HASH, 1f, ANIM_DAMP_TIME, deltaTime);

    }

    private void MoveToPlayer(float deltaTime)
    {
        if (_stateMachine.Agent.isOnNavMesh)
        {
            _stateMachine.Agent.destination = _stateMachine.Player.transform.position;

            Move(_stateMachine.Agent.desiredVelocity.normalized * _stateMachine.MoveSpeed, deltaTime);
        }

        //Keep agent in sync with the Char Contoller
        _stateMachine.Agent.velocity = _stateMachine.Controller.velocity;
    }

    public override void Exit() 
    {
        _stateMachine.Agent.ResetPath();
        _stateMachine.Agent.velocity = Vector3.zero;
    }



}

